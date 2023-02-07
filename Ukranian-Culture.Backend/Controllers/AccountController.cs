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

        if (result is null)
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
    public async Task<IActionResult> ChangeEmail([FromBody]ChangeEmailDto changeEmailDto)
    {
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

    [HttpPost]
    [Route("refreshToken")]
    public async Task<IActionResult> RefreshToken(TokenModel tokenModel)
    {
        if (tokenModel is null)
        {
            return BadRequest();
        }
        var result = await _accountRepository.RefreshToken(tokenModel);
        if(result is null) { return BadRequest(); }
        return Ok(result);
    }
    
    [HttpPost("revoke/{email}")]
    public async Task<IActionResult> Revoke(string email)
    {
        if(string.IsNullOrWhiteSpace(email))
        {
            return BadRequest();
        }
        var result=await _accountRepository.Revoke(email);
        if(!result.Succeeded)
        {
            return BadRequest();
        }
        return Ok(result);
    }
    
    [HttpPost("revokeAll")]
    public async Task<IActionResult> RevokeAll()
    {
        await _accountRepository.RevokeAll();
        return Ok();
    }
}




