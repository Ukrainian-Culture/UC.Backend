using Azure;
using Contracts;
using Entities.DTOs;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ukranian_Culture.Backend.Controllers;

[Route("api/account")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IAccountRepository _accountRepository;

    public AccountController(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    [HttpPost("signup/{email}/{password}/{confirmPassword}")]
    public async Task<IActionResult> SignUp(string email,string password,string confirmPassword)
    {
        var signUpModel = new SignUpUser {
            Email = email,  
            Password = password,
            ConfirmPassword = confirmPassword
        };

        var result = await _accountRepository.SignUpAsync(signUpModel);

        if (result.Succeeded)
        {
            return Ok(result.Succeeded);
        }

        return Unauthorized();
    }

    [HttpPost("login/{email}/{password}")]
    public async Task<IActionResult> Login(string email,string password)
    {
        var signInModel = new SignInUser
        {
            Email = email,
            Password = password
        };
        var result = await _accountRepository.LoginAsync(signInModel);

        if (string.IsNullOrWhiteSpace(result))
        {
            return Unauthorized();
        }

        return Ok(result);
    }


    [HttpPatch("changePassword/{email}/{currentPassword}/{newPassword}/{confirmPassword}")]
    [Authorize]
    public async Task<IActionResult> ChangePassword(string email,string currentPassword,string newPassword,string confirmPassword)
    {
        var changePasswordDto = new ChangePasswordDto
        {
            Email = email,
            CurrentPassword = currentPassword,
            NewPassword = newPassword,
            ConfirmPassword = confirmPassword
        };
        var result = await _accountRepository.ChangePasswordAsync(changePasswordDto);

        if (!result.Succeeded)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPatch("changeEmail/{currentEmail}/{newEmail}")]
    [Authorize]
    public async Task<IActionResult> ChangeEmail(string currentEmail,string newEmail)
    {
        var changeEmailDto = new ChangeEmailDto
        {
            CurrentEmail = currentEmail,
            NewEmail = newEmail
        };
        var result = await _accountRepository.ChangeEmailAsync(changeEmailDto);

        if (!result.Succeeded)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost("logout")]
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        await _accountRepository.Logout();
        return Ok();
    }

    [HttpDelete("deleteAccount/{id:guid}")]
    [Authorize]
    public async Task<IActionResult> DeleteAccount(Guid id)
    {
        var result = await _accountRepository.DeleteAccountAsync(id);
        if (!result.Succeeded) return NotFound();
        return Ok(result);
    }
}




