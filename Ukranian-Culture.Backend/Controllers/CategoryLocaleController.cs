using AutoMapper;
using Contracts;
using Entities.DTOs;
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

        var categories = await _repositoryManager.CategoryLocales
            .GetAllByConditionAsync(catL => catL.CultureId == cultureId, ChangesType.AsNoTracking);
        var categoriesLocalesDtos = _mapper.Map<IEnumerable<CategoryLocaleToGetDto>>(categories);
        return Ok(categoriesLocalesDtos);
    }

    [HttpGet("{id:guid}", Name = "CategoryLocaleById")]
    public async Task<IActionResult> GetCategoryLocaleById(Guid id, Guid cultureId)
    {
        if (await IsCultureExistInDb(cultureId) == false)
            return NotFound(_messageProvider.NotFoundMessage<Culture>(cultureId)); ;

        if (await _repositoryManager
                .CategoryLocales
                .GetFirstByConditionAsync(catL => catL.CategoryId == id && catL.CultureId == cultureId, ChangesType.AsNoTracking)
            is { } categoryLocale)
        {
            var categoryLocaleDto = _mapper.Map<CategoryLocaleToGetDto>(categoryLocale);
            return Ok(categoryLocaleDto);
        }

        var message = _messageProvider.NotFoundMessage<CategoryLocale>(id);
        _logger.LogError(message);
        return NotFound(message);
    }

    [HttpGet("ids")]
    public async Task<IActionResult> GetCategoriesIds(Guid cultureId)
    {
        var categories = await _repositoryManager
            .CategoryLocales
            .GetAllByConditionAsync(cat => cat.CultureId == cultureId, ChangesType.AsNoTracking);

        Dictionary<string, Guid> categoriesIds = categories.ToDictionary(c => c.Name, c => c.CategoryId);
        return Ok(categoriesIds);
    }
    [HttpPost]
    public async Task<IActionResult> CreateCategoryLocale([FromBody] CategoryLocaleToCreateDto? categoryLocaleCreateDto, Guid cultureId)
    {
        if (categoryLocaleCreateDto is null)
        {
            var message = _messageProvider.BadRequestMessage<CategoryLocaleToCreateDto>();
            _logger.LogError(message);
            return BadRequest(message);
        }

        if (await IsCultureExistInDb(cultureId) == false)
            return NotFound(_messageProvider.NotFoundMessage<Culture>(cultureId)); ;

        var categoryEntity = _mapper.Map<CategoryLocale>(categoryLocaleCreateDto);
        _repositoryManager.CategoryLocales.CreateCategoryLocaleForCulture(cultureId, categoryEntity);
        await _repositoryManager.SaveAsync();

        return Ok(categoryEntity);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteCategoryLocale(Guid id, Guid cultureId)
    {
        if (await IsCultureExistInDb(cultureId) == false)
            return NotFound(_messageProvider.NotFoundMessage<Culture>(cultureId)); ;

        var categoryLocale = await _repositoryManager.CategoryLocales
            .GetFirstByConditionAsync(category => category.CategoryId == id && category.CultureId == cultureId, ChangesType.AsNoTracking);

        if (categoryLocale is null)
        {
            var message = _messageProvider.NotFoundMessage<ArticlesLocale>(id);
            _logger.LogInfo(message);
            return NotFound(message);
        }

        _repositoryManager.CategoryLocales.DeleteCategoryLocale(categoryLocale);
        await _repositoryManager.SaveAsync();
        return NoContent();
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateCategoryLocale(Guid id, [FromBody] CategoryLocaleToUpdateDto? categoryLocaleToUpdate, Guid cultureId)
    {
        if (categoryLocaleToUpdate is null)
        {
            var message = _messageProvider.BadRequestMessage<ArticleToUpdateDto>();
            _logger.LogError(message);
            return BadRequest(message);
        }

        if (await IsCultureExistInDb(cultureId) == false)
            return NotFound(_messageProvider.NotFoundMessage<Culture>(cultureId)); ;

        var categoryLocaleEntity = await _repositoryManager.CategoryLocales
            .GetFirstByConditionAsync(categoryLocale => categoryLocale.CategoryId == id && categoryLocale.CultureId == cultureId, ChangesType.Tracking);

        if (categoryLocaleEntity is null)
        {
            var message = _messageProvider.NotFoundMessage<ArticlesLocale>(id);
            _logger.LogInfo(message);
            return NotFound(message);
        }

        _mapper.Map(categoryLocaleToUpdate, categoryLocaleEntity);
        await _repositoryManager.SaveAsync();
        return NoContent();
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