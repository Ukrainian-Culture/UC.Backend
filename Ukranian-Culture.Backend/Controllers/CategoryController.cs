using AutoMapper;
using Contracts;
using Entities.DTOs;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace Ukranian_Culture.Backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly IMapper _mapper;
    private readonly ILoggerManager _logger;

    public CategoryController(IRepositoryManager repositoryManager, IMapper mapper, ILoggerManager logger)
    {
        _repositoryManager = repositoryManager;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCategoriesLocales()
    {
        var categories = await _repositoryManager
            .Categories.GetAllByConditionAsync(_ => true, ChangesType.AsNoTracking);
        var categoriesLocalesDtos = _mapper.Map<IEnumerable<CategoryToGetDto>>(categories);
        return Ok(categoriesLocalesDtos);
    }

    [HttpGet("ids")]
    public async Task<IActionResult> GetCategoriesIds()
    {
        var categories = await _repositoryManager
            .Categories.GetAllByConditionAsync(_ => true, ChangesType.AsNoTracking);

        Dictionary<string, Guid> categoriesIds = categories.ToDictionary(c => c.Name, c => c.Id);
        return Ok(categoriesIds);
    }
}