using Azure;
using Contracts;
using Entities.DTOs;
using Entities.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Owin;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.Connections;

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

    [HttpPost("signup")]
    public async Task<IActionResult> SignUp([FromBody] SignUpUser signUpModel)
    {
        var result = await _accountRepository.SignUpAsync(signUpModel);

        if (result.Succeeded)
        {
            return Ok(result.Succeeded);
        }

        return Unauthorized();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] SignInUser signInModel)
    {
        var result = await _accountRepository.LoginAsync(signInModel);

        if (string.IsNullOrWhiteSpace(result))
        {
            return Unauthorized();
        }

        return Ok(result);
    }

    [HttpPatch("changePassword")]
    [Authorize]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto changePasswordDto)
    {
        var result = await _accountRepository.ChangePasswordAsync(changePasswordDto);

        if (!result.Succeeded)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPatch("changeEmail")]
    [Authorize]
    public async Task<IActionResult> ChangeEmail([FromBody] ChangeEmailDto changeEmailDto)
    {
        var result = await _accountRepository.ChangeEmailAsync(changeEmailDto);

        if (!result.Succeeded)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPatch("changeFirstName")]
    [Authorize]
    public async Task<IActionResult> ChangeFirstName([FromBody] ChangeFirstNameDto changeFirstNameDto)
    {
        var result = await _accountRepository.ChangeFirstNameAsync(changeFirstNameDto);

        if (!result.Succeeded)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPatch("changeLastName")]
    [Authorize]
    public async Task<IActionResult> ChangeLastName([FromBody] ChangeLastNameDto changeLastNameDto)
    {
        var result = await _accountRepository.ChangeLastNameAsync(changeLastNameDto);

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

    [HttpDelete("deleteAccount/{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteAccount(Guid id)
    {
        var result=await _accountRepository.DeleteAccountAsync(id);
        if (!result.Succeeded) return NotFound();
        return Ok(result);
    }
}




