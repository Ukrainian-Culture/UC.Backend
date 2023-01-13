using AutoMapper;
using Contracts;
using Entities.DTOs;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace Ukranian_Culture.Backend.Controllers;

[Route("api/{cultureId:guid}/[controller]")]
[ApiController]
public class ArticlesLocaleController : ControllerBase
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly IMapper _mapper;
    private readonly ILoggerManager _logger;

    public ArticlesLocaleController(IRepositoryManager repositoryManager, IMapper mapper, ILoggerManager logger)
    {
        _repositoryManager = repositoryManager;
        _mapper = mapper;
        _logger = logger;
    }
    [HttpGet]
    public async Task<IActionResult> GetAllArticlesLocales(Guid cultureId)
    {
        return Ok(await _repositoryManager.ArticleLocales
            .GetArticlesLocaleByConditionAsync(artL => artL.CultureId == cultureId, ChangesType.AsNoTracking));
    }

    [HttpGet("{id:guid}", Name = "ArticleLocaleById")]
    public async Task<IActionResult> GetArticleLocaleById(Guid id, Guid cultureId)
    {
        if (await _repositoryManager
                .ArticleLocales
                .GetFirstByConditionAsync(art => art.Id == id && art.CultureId == cultureId, ChangesType.AsNoTracking)
            is { } articleLocale)
        {
            return Ok(articleLocale);
        }

        _logger.LogError($"ArticleLocale with id : \"{id}\" no contain in db");
        return NotFound($"ArticleLocale with id : \"{id}\" no contain in db");
    }

    [HttpPost]
    public async Task<IActionResult> CreateArticleLocale([FromBody] ArticleLocaleToCreateDto? articleLocaleCreateDto, Guid cultureId)
    {
        if (articleLocaleCreateDto is null)
        {
            _logger.LogError("ArticleLocaleToCreateDto object sent from client is null.");
            return BadRequest("ArticleLocaleToCreateDto object is null");
        }

        var culture = await _repositoryManager.Cultures.GetCultureAsync(cultureId, ChangesType.AsNoTracking);
        if (culture is null)
        {
            _logger.LogError($"Culture with this id: \"{cultureId}\" doesn't exist");
            return NotFound($"Culture with this id: \"{cultureId}\" doesn't exist");
        }
        

        var articleEntity = _mapper.Map<ArticlesLocale>(articleLocaleCreateDto);
        _repositoryManager.ArticleLocales.CreateArticlesLocaleForCulture(cultureId, articleEntity);
        await _repositoryManager.SaveAsync();

        return Ok(articleEntity);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteArticleLocale(Guid id, Guid cultureId)
    {
        var culture = await _repositoryManager.Cultures.GetCultureAsync(cultureId, ChangesType.AsNoTracking);
        if (culture is null)
        {
            _logger.LogError($"Culture with this id: \"{cultureId}\" doesn't exist");
            return NotFound($"Culture with this id: \"{cultureId}\" doesn't exist");
        }

        var articleLocale = await _repositoryManager.ArticleLocales
            .GetFirstByConditionAsync(article => article.Id == id && article.CultureId == cultureId, ChangesType.AsNoTracking);

        if (articleLocale is null)
        {
            _logger.LogInfo($"ArticleLocale with id: {id} doesn't exist in the database.");
            return NotFound($"ArticleLocale with id : \"{id}\" no contain in db");
        }

        _repositoryManager.ArticleLocales.DeleteArticlesLocale(articleLocale);
        await _repositoryManager.SaveAsync();
        return NoContent();
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateArticleLocale(Guid id, [FromBody] ArticleLocaleToUpdateDto? articleLocaleToUpdate, Guid cultureId)
    {
        if (articleLocaleToUpdate is null)
        {
            _logger.LogError("articleLocaleToUpdate object sent from client is null.");
            return BadRequest("articleLocaleToUpdate object is null");
        }

        var culture = await _repositoryManager.Cultures.GetCultureAsync(cultureId, ChangesType.AsNoTracking);
        if (culture is null)
        {
            _logger.LogError($"Culture with this id: \"{cultureId}\" doesn't exist");
            return NotFound($"Culture with this id: \"{cultureId}\" doesn't exist");
        }

        var articleEntity = await _repositoryManager.ArticleLocales
            .GetFirstByConditionAsync(art => art.Id == id && art.CultureId == cultureId, ChangesType.Tracking);

        if (articleEntity is null)
        {
            _logger.LogInfo($"ArticleLocale with id: {id} doesn't exist in the database.");
            return NotFound($"ArticleLocale with id : \"{id}\" no contain in db");
        }

        _mapper.Map(articleLocaleToUpdate, articleEntity);
        await _repositoryManager.SaveAsync();
        return NoContent();
    }
}