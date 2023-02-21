﻿using System.Globalization;
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
    private readonly IRepositoryManager _repositoryManager;
    private readonly IErrorMessageProvider _messageProvider;
    private readonly IDateTimeProvider _dateTimeProvider;

    public AccountController(IAccountRepository accountRepository, IRepositoryManager repositoryManager,
        IErrorMessageProvider messageProvider, IDateTimeProvider dateTimeProvider)
    {
        _accountRepository = accountRepository;
        _repositoryManager = repositoryManager;
        _messageProvider = messageProvider;
        _dateTimeProvider = dateTimeProvider;
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
    public async Task<IActionResult> ChangeEmail([FromBody] ChangeEmailDto changeEmailDto)
    {
        var result = await _accountRepository.ChangeEmailAsync(changeEmailDto);

        if (!result.Succeeded)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPatch("ChangeEndOfSub")]
    public async Task<IActionResult> ChangeEndOfSub([FromBody] ChangeEndOfSubscriptionDto endSubDto)
    {
        var user = await _repositoryManager
            .Users
            .GetFirstByConditionAsync(user => user.Email == endSubDto.Email, ChangesType.Tracking);

        if (user is null)
        {
            return NotFound(_messageProvider.NotFoundMessage<User, string>(endSubDto.Email));
        }

        user.SubscriptionEndDate = endSubDto.NewEndOfSubscription;
        await _repositoryManager.SaveAsync();
        return NoContent();
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
    public async Task<IActionResult> RefreshToken(TokenModel? tokenModel)
    {
        if (tokenModel is null)
        {
            return BadRequest();
        }

        var result = await _accountRepository.RefreshToken(tokenModel);
        if (result is null)
        {
            return BadRequest();
        }

        return Ok(result);
    }

    [HttpPost("revoke/{email}")]
    public async Task<IActionResult> Revoke(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            return BadRequest();
        }

        var result = await _accountRepository.Revoke(email);
        if (!result.Succeeded)
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

    [HttpGet("ConfirmEmail")]
    public async Task<IActionResult> ConfirmEmail(string email, string token)
    {
        var result = await _accountRepository.ConfirmEmailAsync(email, token);
        if (!result.Succeeded)
        {
            return NotFound();
        }

        return Ok("Your email was confirmed");
    }

    [HttpGet("endDate/{email}")]
    public async Task<IActionResult> GetSubscriptionEndDate(string email)
    {
        var user = await _repositoryManager
            .Users
            .GetFirstByConditionAsync(user => user.Email == email, ChangesType.AsNoTracking);
        if (user is null)
        {
            return NotFound(_messageProvider.NotFoundMessage<User, string>(email));
        }

        if (_dateTimeProvider.GetCurrentTime() > user.SubscriptionEndDate)
        {
            return Ok("00:00");
        }

        var timeLeft = user.SubscriptionEndDate - _dateTimeProvider.GetCurrentTime();
        return Ok(timeLeft.ToString(@"dd\:hh", CultureInfo.CurrentCulture));
    }
}