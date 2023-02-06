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
    private readonly IArticleTileService _articleTilesService;

    public ArticlesTileController(IArticleTileService articleTilesService)
    {
        _articleTilesService = articleTilesService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllArticlesOnLanguage(Guid cultureId) =>
        Ok(await _articleTilesService.TryGetArticleTileDto(cultureId, _ => true));

    [HttpGet("{regionName}")]
    public async Task<IActionResult> GetArticlesByRegion(Guid cultureId, string regionName) =>
        Ok(await _articleTilesService.TryGetArticleTileDto(cultureId, article => article.Region == regionName));

    [HttpGet("{categoryId:Guid}")]
    public async Task<IActionResult> GetArticlesByCategory(Guid cultureId, Guid categoryId) =>
        Ok(await _articleTilesService.TryGetArticleTileDto(cultureId,
            article => article.CategoryId == categoryId));

    [HttpGet("{regionName}/{categoryId:guid}")]
    public async Task<IActionResult> GetArticlesTileByRegionAndCategory(string regionName, Guid categoryId,
        Guid cultureId) =>
        Ok(await _articleTilesService.TryGetArticleTileDto(cultureId,
            article => article.CategoryId == categoryId && article.Region == regionName));

    [HttpGet("categoriesMap")]
    public async Task<IActionResult> GetArticlesTileForEveryCategory(Guid cultureId)
    {
        var articlesTiles = await _articleTilesService.TryGetArticleTileDto(cultureId, _ => true);
        var tileDictionary = articlesTiles
            .GroupBy(articleTile => articleTile.Category)
            .ToDictionary(group => group.Key, group => group.ToList());

        return Ok(tileDictionary);
    }
}