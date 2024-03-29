﻿using Entities.DTOs;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Contracts;

public interface IAccountRepository
{
    Task<IdentityResult> SignUpAsync(SignUpUser signUpModel);
    Task<TokenModel> LoginAsync(SignInUser signInModel);
    Task<IdentityResult> ChangePasswordAsync(ChangePasswordDto changePasswordDto);
    Task<IdentityResult> ChangeEmailAsync(ChangeEmailDto changeEmailDto);
    Task Logout();
    Task<IdentityResult> DeleteAccountAsync(Guid id);
    Task<TokenModel> RefreshToken(TokenModel tokenModel);
    Task<IdentityResult> Revoke(string email);
    Task RevokeAll();
    Task<IdentityResult> ConfirmEmailAsync(string email, string token);
    Task<IdentityResult> ResetPasswordAsync(ResetPasswordDto resetPassword);
    Task<string> GetTokenForgotPasswordAsync(string email, string url);
    Task<string> GetTokenSendEmailAsync(string email, string url);
    Task DeleteFailedUsers();
}

