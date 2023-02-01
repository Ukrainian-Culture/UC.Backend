using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Security.Principal;
using AutoMapper;
using Contracts;
using Entities.DTOs;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Ukranian_Culture.Backend.Controllers;

namespace Ukranian_Culture.Backend.Controllers;

[Route("api/{cultureId:Guid}/[controller]")]
[ApiController]
public class ArticlesTileController : ControllerBase
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly IMapper _mapper;
    private readonly ILoggerManager _logger;

    public ArticlesTileController(IRepositoryManager repositoryManager, IMapper mapper, ILoggerManager logger)
    {
        _repositoryManager = repositoryManager;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllArticlesOnLanguage(Guid cultureId)
    {
        try
        {
            return Ok(await TryGetArticleTileDto(cultureId, _ => true));
        }
        catch (ArgumentNullException ex)
        {
            _logger.LogError(ex.Message);
            return NotFound();
        }
    }

    [HttpGet("{regionName}")]
    public async Task<IActionResult> GetArticlesByRegion(Guid cultureId, string regionName)
    {
        try
        {
            return Ok(await TryGetArticleTileDto(cultureId, article => article.Region == regionName));
        }
        catch (ArgumentNullException ex)
        {
            _logger.LogError(ex.Message);
            return NotFound();
        }
    }

    [HttpGet("{categoryId:Guid}")]
    public async Task<IActionResult> GetArticlesByCategory(Guid cultureId, Guid categoryId)
    {
        try
        {
            return Ok(await TryGetArticleTileDto(cultureId, article => article.CategoryId == categoryId));
        }
        catch (ArgumentNullException ex)
        {
            _logger.LogError(ex.Message);
            return NotFound();
        }
    }

    [HttpGet("{regionName}/{categoryId:guid}")]
    public async Task<IActionResult> GetArticlesTileByRegionAndCategory(string regionName, Guid categoryId,
        Guid cultureId)
    {
        try
        {
            return Ok(await TryGetArticleTileDto(cultureId,
                article => article.CategoryId == categoryId && article.Region == regionName));
        }
        catch (ArgumentNullException ex)
        {
            _logger.LogError(ex.Message);
            return NotFound();
        }
    }

    [HttpGet("categoriesMap")]
    public async Task<IActionResult> GetArticlesTileForEveryCategory(Guid cultureId)
    {
        var articlesTiles = await TryGetArticleTileDto(cultureId, _ => true);
        var tileDictionary = articlesTiles
            .GroupBy(articleTile => articleTile.Category)
            .ToDictionary(group => group.Key, group => group.ToList());

        return Ok(tileDictionary);
    }

    private async Task<IEnumerable<ArticleTileDto>> TryGetArticleTileDto(Guid cultureId,
        Expression<Func<Article, bool>> conditionToFindArticles)
    {
        var articlesLocales = await GetArticlesLocaleByCondition(artL => artL.CultureId == cultureId);
        var articles = await GetArticlesByConditionAsync(conditionToFindArticles);
        var result = await CreateArticleTileDtos(articlesLocales, articles);
        return result;
    }


    private Task<List<ArticleTileDto>> CreateArticleTileDtos(IEnumerable<ArticlesLocale> articlesLocale,
        IEnumerable<Article> articles)
    {
        var articleTiles = new List<ArticleTileDto>();
        foreach (var article in articles)
        {
            ArticlesLocale? correctArticleLocale = articlesLocale
                .FirstOrDefault(artL => artL.Id == article.Id);
            if (correctArticleLocale is not null)
            {
                articleTiles.Add(_mapper.Map<ArticleTileDto>((article, correctArticleLocale)));
                continue;
            }

            _logger.LogError($"article with id:\"{article.Id}\" has no translate, or {article.CategoryId} is invalid");
        }

        return Task.FromResult(articleTiles);
    }


    private async Task<IEnumerable<Article>> GetArticlesByConditionAsync(Expression<Func<Article, bool>> expression)
        => await _repositoryManager
            .Articles
            .GetAllByConditionAsync(expression, ChangesType.AsNoTracking);

    private async Task<IEnumerable<ArticlesLocale>> GetArticlesLocaleByCondition(
        Expression<Func<ArticlesLocale, bool>> func)
        => await _repositoryManager
            .ArticleLocales
            .GetArticlesLocaleByConditionAsync(func, ChangesType.AsNoTracking);
}