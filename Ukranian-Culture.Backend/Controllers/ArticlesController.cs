using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Contracts;
using Entities.DTOs;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Ukranian_Culture.Backend.Controllers;

[Route("api/{cultureId:int}/[controller]")]
[ApiController]
public class ArticlesController : ControllerBase
{
    private readonly IRepositoryManager _repositoryManager;

    public ArticlesController(IRepositoryManager repositoryManager)
    {
        _repositoryManager = repositoryManager;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllArticlesOnLanguage(int cultureId)
    {

        var culture = await GetCultureDataAsync(cultureId);
        var articles = await GetArticlesByConditionAsync(_ => true);
        var result = await JoinArticleAndArticleLocalInOneModel(culture, articles);
        return Ok(result);
    }

    [HttpGet("{regionName}")]
    public async Task<IActionResult> GetArticlesByRegion(int cultureId, string regionName)
    {
        var culture = await GetCultureDataAsync(cultureId);
        var articles = await GetArticlesByConditionAsync(article => article.Region == regionName);
        var result = await JoinArticleAndArticleLocalInOneModel(culture, articles);
        return Ok(result);
    }

    private async Task<IEnumerable<ArticleTileDto>> JoinArticleAndArticleLocalInOneModel(Culture culture, IEnumerable<Article> articles)
    {
        return culture
            .ArticlesTranslates
            .Join(articles,
                articleLocale => articleLocale.Id,
                article => article.Id,
                await CreateArticleTileDto(culture.Id));
    }
    private async Task<Func<ArticlesLocale, Article, ArticleTileDto>> CreateArticleTileDto(int cultureId)
    {
        var categories = await GetCategories(cultureId);

        return (articleLocale, article) => new ArticleTileDto
        {
            ArticleId = article.Id,
            Type = article.Type,
            Region = article.Region,
            SubText = articleLocale.SubText,
            Title = articleLocale.Title,
            Category = categories[article.CategoryId]
        };
    }
    private async Task<Dictionary<int, string>> GetCategories(int cultureId)
    {
        return (await _repositoryManager.CategoryLocales.GetCategoriesByCulture(cultureId, ChangesType.AsNoTracking))
            .ToDictionary(a => a.CategoryId, a => a.Name);
    }
    private async Task<IEnumerable<Article>> GetArticlesByConditionAsync(Expression<Func<Article, bool>> expression)
    {
        return await _repositoryManager
            .Articles
            .GetAllByConditionAsync(expression, ChangesType.AsNoTracking);
    }
    private async Task<Culture> GetCultureDataAsync(int cultureId)
    {
        return await _repositoryManager.Cultures.GetCultureWithContentAsync(cultureId, ChangesType.AsNoTracking);
    }
}