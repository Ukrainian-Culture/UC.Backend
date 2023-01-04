using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Security.Principal;
using Contracts;
using Entities.DTOs;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Ukranian_Culture.Backend.Controllers;
using Mappers;
namespace Ukranian_Culture.Backend.Controllers;

[Route("api/{cultureId:int}/[controller]")]
[ApiController]
public class ArticlesTileController : ControllerBase
{
    private readonly IRepositoryManager _repositoryManager;

    public ArticlesTileController(IRepositoryManager repositoryManager)
    {
        _repositoryManager = repositoryManager;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllArticlesOnLanguage(int cultureId)
    {

        var culture = await GetCultureDataAsync(cultureId);
        var articles = await GetArticlesByConditionAsync(_ => true);
        var result = await CreateArticleTileDtos(culture, articles);
        return Ok(result);
    }

    [HttpGet("{regionName}")]
    public async Task<IActionResult> GetArticlesByRegion(int cultureId, string regionName)
    {
        var culture = await GetCultureDataAsync(cultureId);
        var articles = await GetArticlesByConditionAsync(article => article.Region == regionName);
        var result = await CreateArticleTileDtos(culture, articles);
        return Ok(result);
    }

    [HttpGet("{categoryId:int}")]
    public async Task<IActionResult> GetArticlesByCategory(int cultureId, int categoryId)
    {
        var culture = await GetCultureDataAsync(cultureId);
        var articles = await GetArticlesByConditionAsync(article => article.CategoryId == categoryId);
        var result = await CreateArticleTileDtos(culture, articles);
        return Ok(result);
    }

    private async Task<IEnumerable<ArticleTileDto>> CreateArticleTileDtos(Culture culture, IEnumerable<Article> articles)
    {
        var articlesLocale = culture.ArticlesTranslates.ToList();
        var categories = culture.Categories.ToDictionary(a => a.CategoryId, a => a.Name);

        return await new MapperArticleTileDto()
            .MappedTwoModelsInOne(articles, articlesLocale, categories);
    }

    private async Task<IEnumerable<Article>> GetArticlesByConditionAsync(Expression<Func<Article, bool>> expression)
        => await _repositoryManager
            .Articles
            .GetAllByConditionAsync(expression, ChangesType.AsNoTracking);

    private async Task<Culture> GetCultureDataAsync(int cultureId)
        => await _repositoryManager
            .Cultures
            .GetCultureWithContentAsync(cultureId, ChangesType.AsNoTracking);

}