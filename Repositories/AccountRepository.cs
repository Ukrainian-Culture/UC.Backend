using Contracts;
using Entities.DTOs;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Authorization;

namespace Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IConfiguration _configuration;

    public AccountRepository(UserManager<User> userManager,
        SignInManager<User> signInManager,
        IConfiguration configuration)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _configuration = configuration;
    }
    public async Task<TokenModel> LoginAsync(SignInUser signInModel)
    {
        var userByEmail = await _userManager.FindByEmailAsync(signInModel.Email);
        if (userByEmail is null) return null;
        var result = await _signInManager.PasswordSignInAsync(userByEmail.UserName, signInModel.Password, false, false);
        if (!result.Succeeded) return null;

        var authClaims = new List<Claim>
        {
            new Claim(ClaimTypes.Email,signInModel.Email),
            new Claim(ClaimTypes.Name,userByEmail.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var roles = await _userManager.GetRolesAsync(userByEmail);
        AddRolesToClaims(authClaims, roles);

        var token = CreateToken(authClaims);
        var refreshToken = GenerateRefreshToken();

        _ = int.TryParse(_configuration["JWT:RefreshTokenValidityInDays"], out int refreshTokenValidityInDays);

        userByEmail.RefreshToken = refreshToken;
        userByEmail.RefreshTokenExpiryTime = DateTime.Now.AddDays(refreshTokenValidityInDays);

        await _userManager.UpdateAsync(userByEmail);

        return new TokenModel
        {
            AccessToken = token,
            RefreshToken = refreshToken
        };
    }

    public async Task<IdentityResult> SignUpAsync(SignUpUser signUpModel)
    {
        string userName = signUpModel.Email.Substring(0, signUpModel.Email.IndexOf("@"));

        var user = new User()
        {
            Email = signUpModel.Email,
            UserName = userName,
            EmailConfirmed = false
        };

        var result = await _userManager.CreateAsync(user, signUpModel.Password);
        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(user, "User");
            await _userManager.UpdateAsync(user);
            var token = Convert.ToBase64String(Encoding.UTF8.GetBytes(await _userManager.GenerateEmailConfirmationTokenAsync(user)));
            var url = $"https://ucbackend.azurewebsites.net/api/account/ConfirmEmail?email={user.Email}&token={token}";
            var body = "Click the link below to confirm your email.<br>" + url;
            await SendEmailAsync(user.Email, "Confirm email", "", body);
        }
        return result;
    }

    public async Task<IdentityResult> Revoke(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user is null)
        {
            return IdentityResult.Failed();
        }
        user.RefreshToken = null;
        return await _userManager.UpdateAsync(user);
    }

    public async Task RevokeAll()
    {
        var users = _userManager.Users.ToList();
        foreach (var user in users)
        {
            user.RefreshToken = null;
            await _userManager.UpdateAsync(user);
        }
    }

    public async Task<IdentityResult> ChangePasswordAsync(ChangePasswordDto changePasswordDto)
    {
        var user = await _userManager.FindByEmailAsync(changePasswordDto.Email);
        if (user == null)
        {
            var resultFailed = IdentityResult.Failed();
            return resultFailed;
        }
        var result = await _userManager.ChangePasswordAsync(user, changePasswordDto.CurrentPassword, changePasswordDto.NewPassword);
        return result;
    }

    public async Task<IdentityResult> ChangeEmailAsync(ChangeEmailDto changeEmailDto)
    {
        var user = await _userManager.FindByEmailAsync(changeEmailDto.CurrentEmail);
        if (user == null)
        {
            return IdentityResult.Failed();
        }
        var token = await _userManager.GenerateChangeEmailTokenAsync(user, changeEmailDto.NewEmail);
        var result = await _userManager.ChangeEmailAsync(user, changeEmailDto.NewEmail, token);
        string userName = user.Email.Substring(0, user.Email.IndexOf("@"));
        user.UserName = userName;
        await _userManager.UpdateAsync(user);
        return result;
    }

    public Task Logout()
    {
        return _signInManager.SignOutAsync();
    }

    public async Task<IdentityResult> DeleteAccountAsync(Guid id)
    {
        var user = _userManager.Users.Where(x => x.Id == id).FirstOrDefault();
        if (user != null)
        {
            var result = await _userManager.DeleteAsync(user);
            return result;
        }
        return IdentityResult.Failed();

    }
    private string CreateToken(List<Claim> authClaims)
    {
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
        _ = int.TryParse(_configuration["JWT:TokenValidityInMinutes"], out int tokenValidityInMinutes);

        var token = new JwtSecurityToken(
            issuer: _configuration["JWT:ValidIssuer"],
            audience: _configuration["JWT:ValidAudience"],
            claims: authClaims,
            expires: DateTime.Now.AddMinutes(tokenValidityInMinutes),
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    private static string GenerateRefreshToken()
    {
        var randomNumber = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    private void AddRolesToClaims(List<Claim> claims, IEnumerable<string> roles)
    {
        foreach (var role in roles)
        {
            var roleClaim = new Claim(ClaimTypes.Role, role);
            claims.Add(roleClaim);
        }
    }
    public async Task<TokenModel> RefreshToken(TokenModel tokenModel)
    {
        string? accessToken = tokenModel.AccessToken;
        string? refreshToken = tokenModel.RefreshToken;

        var principal = GetPrincipalFromExpiredToken(accessToken);
        string username = principal.Identity.Name;

        var user = await _userManager.FindByNameAsync(username);

        if (user is null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now) { return null; }

        var newAccessToken = CreateToken(principal.Claims.ToList());
        var newRefreshToken = GenerateRefreshToken();

        user.RefreshToken = newRefreshToken;
        await _userManager.UpdateAsync(user);

        return new TokenModel
        {
            AccessToken = newAccessToken,
            RefreshToken = newRefreshToken
        };
    }
    private ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"])),
            ValidateLifetime = false
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
        if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            throw new SecurityTokenException("Invalid token");

        return principal;
    }
    public async Task<IdentityResult> ConfirmEmailAsync(string email, string token)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
        {
            return IdentityResult.Failed();
        }
        var result = await _userManager.ConfirmEmailAsync(user, Encoding.UTF8.GetString(Convert.FromBase64String(token)));
        return result;
    }
    public async Task SendEmailAsync(string toEmail, string subject, string plainTextContent, string content)
    {
        var apiKey = "APIKey";
        var client = new SendGridClient(apiKey);
        var from = new EmailAddress("ukrainianculture938@gmail.com", "UC");
        var to = new EmailAddress(toEmail, "New User");
        var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, content);
        var response = await client.SendEmailAsync(msg);
    }
}
