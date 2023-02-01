﻿using Entities.DTOs;
using Entities.Models;
using Microsoft.AspNetCore.Identity;

namespace Contracts;

public interface IAccountRepository
{
    Task<IdentityResult> SignUpAsync(SignUpUser signUpModel);
    Task<string> LoginAsync(SignInUser signInModel);
    Task<IdentityResult> ChangePasswordAsync(ChangePasswordDto changePasswordDto);
    Task<IdentityResult> ChangeEmailAsync(ChangeEmailDto changeEmailDto);
    Task<IdentityResult> ChangeFirstNameAsync(ChangeFirstNameDto changeFirstNameDto);
    Task<IdentityResult> ChangeLastNameAsync(ChangeLastNameDto changeLastNameDto);
    Task Logout();
    Task<IdentityResult> DeleteAccountAsync(Guid id);
}

