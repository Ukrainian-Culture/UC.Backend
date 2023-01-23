using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;

namespace Contracts;

public interface IAccountRepository
{
    Task<IdentityResult> SignUpAsync(SignUpUser signUpModel, HttpContext httpContext, IUrlHelper url);
    Task<string> LoginAsync(SignInUser signInModel);
    Task<bool> ConfirmEmailAsync(string userId, string code);
}

