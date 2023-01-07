using AutoMapper;
using Contracts;
using Entities.DTOs;
using Microsoft.AspNetCore.Mvc;
using UkranianCulture.Backend;

namespace Ukranian_Culture.Backend.Controllers;

[Route("api/{culture}/[controller]")]
[ApiController]
public class HistoryController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IRepositoryManager _repositoryManager;
    private readonly ILoggerManager _logger;

    public HistoryController(IMapper mapper, IRepositoryManager repositoryManager, ILoggerManager logger)
    {
        _mapper = mapper;
        _repositoryManager = repositoryManager;
        _logger = logger;
    }

    [HttpGet("{region}")]
    public async Task<IActionResult> GetHistoryByRegion(int culture, string region)
    {
        var articles = await _repositoryManager
            .Articles
            .GetAllByConditionAsync(art => art.Region == region, ChangesType.AsNoTracking);

        if (!articles.Any())
        {
            _logger.LogError("Articles are absent");
            return BadRequest();
        }

        var articlesLocale = articles
            .Select(art => art.Id)
            .Select(id => _repositoryManager
                    .ArticleLocales
                    .GetArticlesLocaleByConditionAsync(artl => artl.Id == id && artl.CultureId == culture, ChangesType.AsNoTracking)
                        .Result.FirstOrDefault()
            ).ToList();

        if (articlesLocale.Contains(null))
        {
            _logger.LogError("ArticlesLocale are absent");
            return BadRequest();
        }

        var history = articles.Select((art, i) => _mapper.Map<HistoryDto>((art, articlesLocale[i])));

        return Ok(history);
    }
}