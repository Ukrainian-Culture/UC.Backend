using System.Linq.Expressions;
using AutoMapper;
using Contracts;
using Entities.DTOs;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;

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
        catch (Exception ex)
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
        catch (Exception ex)
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
        catch (Exception ex)
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
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return NotFound();
        }
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetArticlesTileForEveryCategory(Guid cultureId)
    {
        var articles = await TryGetArticleTileDto(cultureId, _ => true);

        Dictionary<string, List<ArticleTileDto>> tileDictionary = articles
            .GroupBy(a => a.Category)
            .ToDictionary(a => a.Key, a => a.ToList());

        return Ok(tileDictionary);
    }

    private async Task<IEnumerable<ArticleTileDto>> TryGetArticleTileDto(Guid cultureId,
        Expression<Func<Article, bool>> conditionToFindArticles)
    {
        var culture = await GetCultureDataAsync(cultureId);
        var articles = await GetArticlesByConditionAsync(conditionToFindArticles);
        var result = await CreateArticleTileDtos(culture, articles);
        return result;
    }

    private Task<List<ArticleTileDto>> CreateArticleTileDtos(Culture culture, IEnumerable<Article> articles)
    {
        var articlesLocale = culture.ArticlesTranslates.ToList();

        var articleTiles = new List<ArticleTileDto>();
        foreach (var article in articles)
        {
            ArticlesLocale? correctArticleLocale = articlesLocale.FirstOrDefault(artL => artL.Id == article.Id);
            bool isArticleHaveCorrectCategory
                = culture.Categories
                    .Select(cat => cat.CategoryId)
                    .Contains(article.CategoryId);

            if (correctArticleLocale is not null && isArticleHaveCorrectCategory)
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

    private async Task<Culture> GetCultureDataAsync(Guid cultureId)
        => await _repositoryManager
            .Cultures
            .GetCultureWithContentAsync(cultureId, ChangesType.AsNoTracking);
}