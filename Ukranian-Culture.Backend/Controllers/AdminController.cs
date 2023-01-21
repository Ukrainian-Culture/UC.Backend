using Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ukranian_Culture.Backend.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Admin")]
public class AdminController : ControllerBase
{
    private readonly IRepositoryManager _repository;

    public AdminController(IRepositoryManager repository)
    {
        _repository = repository;
    }

    [HttpGet("articlesCount")]
    public async Task<int> GetArticlesCount()
    {
        return await _repository.Articles.CountAsync;
    }

    [HttpGet("usersCount")]
    public async Task<int> GetUsersCount()
    {
        return await _repository.Users.CountAsync;
    }
}