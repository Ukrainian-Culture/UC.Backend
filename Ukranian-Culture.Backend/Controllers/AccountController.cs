using Contracts;
using Entities.Models;
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

        if (string.IsNullOrWhiteSpace(result))
        {
            return Unauthorized();
        }

        return Ok(result);
    }
}



