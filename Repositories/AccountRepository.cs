using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
    public async Task<string> LoginAsync(SignInUser signInModel)
    {
        var result = await _signInManager.PasswordSignInAsync(signInModel.FirstName, signInModel.Password, false, false);
        if (!result.Succeeded) return string.Empty;

        var authClaims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, signInModel.FirstName),
            new Claim(ClaimTypes.Email,signInModel.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };
        var user = _userManager.Users.FirstOrDefault(x => x.Email == signInModel.Email);

        var roles = await _userManager.GetRolesAsync(user);
        AddRolesToClaims(authClaims, roles);
        var authSigninKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JWT:Secret"]));

        var token = new JwtSecurityToken(
            issuer: _configuration["JWT:ValidIssuer"],
            audience: _configuration["JWT:ValidAudience"],
            expires: DateTime.Now.AddDays(1),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigninKey, SecurityAlgorithms.HmacSha256Signature)
            );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public async Task<IdentityResult> SignUpAsync(SignUpUser signUpModel)
    {
        var user = new User()
        {
            FirstName = signUpModel.FirstName,
            LastName = signUpModel.LastName,
            Email = signUpModel.Email,
            UserName = signUpModel.FirstName,
            EmailConfirmed = false
        };

        var result = await _userManager.CreateAsync(user, signUpModel.Password);
        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(user, "User");
            await _userManager.UpdateAsync(user);
        }
        return result;
    }
    private void AddRolesToClaims(List<Claim> claims, IEnumerable<string> roles)
    {
        foreach (var role in roles)
        {
            var roleClaim = new Claim(ClaimTypes.Role, role);
            claims.Add(roleClaim);
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
        if (user != null)
        {
            var token = await _userManager.GenerateChangeEmailTokenAsync(user, changeEmailDto.NewEmail);
            var result = await _userManager.ChangeEmailAsync(user, changeEmailDto.NewEmail, token);
            return result;
        }
        return IdentityResult.Failed();
    }

    public async Task<IdentityResult> ChangeFirstNameAsync(ChangeFirstNameDto changeFirstNameDto)
    {
        var user = await _userManager.FindByEmailAsync(changeFirstNameDto.Email);
        if(user == null)
        {
            var resultFailed=IdentityResult.Failed();
            return resultFailed;
        }
        user.FirstName = changeFirstNameDto.NewFirstName;
        user.UserName = changeFirstNameDto.NewFirstName;
        var result = await _userManager.UpdateAsync(user);
        return result;
    }

    public async Task<IdentityResult> ChangeLastNameAsync(ChangeLastNameDto changeLastNameDto)
    {
        var user = await _userManager.FindByEmailAsync(changeLastNameDto.Email);
        if (user != null)
        {
            user.LastName = changeLastNameDto.NewLastName;
            var result = await _userManager.UpdateAsync(user);
            return result;
        }
        return IdentityResult.Failed();
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
}
