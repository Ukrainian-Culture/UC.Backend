using Entities.DTOs;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Contracts;

public interface IAccountRepository
{
    Task<IdentityResult> SignUpAsync(SignUpUser signUpModel, HttpContext httpContext, IUrlHelper url);
    Task<string> LoginAsync(SignInUser signInModel);
    Task<IdentityResult> ChangePasswordAsync(ChangePasswordDto changePasswordDto);
    Task<IdentityResult> ChangeEmailAsync(ChangeEmailDto changeEmailDto);
    Task<IdentityResult> ChangeFirstNameAsync(ChangeFirstNameDto changeFirstNameDto);
    Task<IdentityResult> ChangeLastNameAsync(ChangeLastNameDto changeLastNameDto);
    Task Logout();
    Task<IdentityResult> DeleteAccountAsync(Guid id);
    Task<bool> ConfirmEmailAsync(Guid userId, string code);
}

