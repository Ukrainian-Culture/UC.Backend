using Entities.Models;
using Microsoft.AspNetCore.Identity;

namespace Contracts;

public interface IAccountRepository
{
    Task<IdentityResult> SignUpAsync(SignUpUser signUpModel, string role);
    Task<string> LoginAsync(SignInUser signInModel);
}

