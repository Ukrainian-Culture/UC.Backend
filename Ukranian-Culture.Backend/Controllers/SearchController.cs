using Contracts;
using Entities.SearchEngines;
using Microsoft.AspNetCore.Mvc;

namespace Ukranian_Culture.Backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SearchController : ControllerBase
{
    private readonly IArticleTileService _articleTilesService;

    public SearchController(IArticleTileService articleTilesService)
    {
        _articleTilesService = articleTilesService;
    }

    [HttpGet("articleTiles/{cultureId:guid}/{stringTerm}")]
    public async Task<IActionResult> Search(Guid cultureId, string stringTerm)
    {
        ArticleTileSearchEngine engine = new();
        var articles = await _articleTilesService.TryGetArticleTileDto(cultureId, _ => true);

        engine.AddArticlesTileToIndex(articles);
        var result = engine.Search(stringTerm);
        return Ok(result);
    }
}