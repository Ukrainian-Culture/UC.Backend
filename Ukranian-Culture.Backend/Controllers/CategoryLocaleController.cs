using AutoMapper;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace Ukranian_Culture.Backend.Controllers;

[Route("api/{cultureId:guid}/[controller]")]
[ApiController]
public class CategoryLocaleController : ControllerBase
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly IMapper _mapper;
    private readonly ILoggerManager _logger;
    private readonly IErrorMessageProvider _messageProvider;

    public CategoryLocaleController(IRepositoryManager repositoryManager, IMapper mapper, ILoggerManager logger, IErrorMessageProvider messageProvider)
    {
        _repositoryManager = repositoryManager;
        _mapper = mapper;
        _logger = logger;
        _messageProvider = messageProvider;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCategoriesLocales(Guid cultureId)
    {
        if (await IsCultureExistInDb(cultureId) == false)
            return NotFound(_messageProvider.NotFoundMessage<Culture>(cultureId)); ;

        return Ok(await _repositoryManager.CategoryLocales
            .GetAllByConditionAsync(catL => catL.CultureId == cultureId, ChangesType.AsNoTracking));
    }

    [HttpGet("{id:guid}", Name = "CategoryLocaleById")]
    public async Task<IActionResult> GetCategoryLocaleById(Guid id, Guid cultureId)
    {
        if (await IsCultureExistInDb(cultureId) == false)
            return NotFound(_messageProvider.NotFoundMessage<Culture>(cultureId)); ;

        if (await _repositoryManager
                .CategoryLocales
                .GetFirstByCondition(catL => catL.CategoryId == id && catL.CultureId == cultureId, ChangesType.AsNoTracking)
            is { } categoryLocale)
        {
            return Ok(categoryLocale);
        }

        var message = _messageProvider.NotFoundMessage<CategoryLocale>(id);
        _logger.LogError(message);
        return NotFound(message);
    }
    private async Task<bool> IsCultureExistInDb(Guid cultureId)
    {
        var culture = await _repositoryManager.Cultures.GetCultureAsync(cultureId, ChangesType.AsNoTracking);
        if (culture is not null)
        {
            return true;
        }
        _logger.LogError(_messageProvider.NotFoundMessage<Culture>(cultureId));
        return false;
    }
}