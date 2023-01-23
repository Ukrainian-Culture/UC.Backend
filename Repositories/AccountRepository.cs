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

    public async Task<IdentityResult> SignUpAsync(SignUpUser signUpModel, HttpContext httpContext, IUrlHelper url)
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
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            await SendEmail(user.Email, user.Id, code, httpContext, url);
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
    public async Task<bool> ConfirmEmailAsync(Guid userId, string code)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user == null)
        {
            return false;
        }
        var result = await _userManager.ConfirmEmailAsync(user, code);
        return result.Succeeded;
    }
    static async Task SendEmail(string email, Guid userId, string code, HttpContext httpContext, IUrlHelper url)
    {
        var emailBody = "<a href=\"#URL#\">Click here</a>";
        var callbackUrl = httpContext.Request.Scheme + "://" + httpContext.Request.Host + url.Action("ConfirmEmail", "Account", new { userId = userId, code = code });
        var body = emailBody.Replace("#URL#", callbackUrl);
        var apiKey = "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";
        var client = new SendGridClient(apiKey);
        var from = new EmailAddress("UkrainianCulture@mail.com", "UC");
        var subject = "Confrim your email";
        var to = new EmailAddress(email, "New User");
        var plainTextContent = "Confirm your email address";
        var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, body);
        var response = await client.SendEmailAsync(msg);
        
    }
}
