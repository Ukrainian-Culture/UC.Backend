using AutoMapper;
using Contracts;
using Entities.DTOs;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace Ukranian_Culture.Backend.Controllers;

[Route("api/{culture}/[controller]")]
[ApiController]
public class HistoryController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IRepositoryManager _repositoryManager;

    public HistoryController(IMapper mapper, IRepositoryManager repositoryManager)
    {
        _mapper = mapper;
        _repositoryManager = repositoryManager;
    }

    [HttpGet("{region}")]
    public async Task<IActionResult> GetHistoryByRegion(int culture, string region)
    {
        var articles = await _repositoryManager.Articles.GetArticlesByConditionAsync(art => art.Region == region, ChangesType.AsNoTracking);

        if (!articles.Any()) throw new Exception("Articles are absent");

        var articlesLocale = (await articles
            .Select(art => art.Id)
            .Select( async id => await _repositoryManager.ArticleLocales.GetArticlesLocaleByConditionAsync(artl => artl.Id == id && artl.CultureId == culture, ChangesType.AsNoTracking)).First())
            .ToList();

        if (!articlesLocale.Any()) throw new Exception("ArticlesLocale are absent");

        var history = articles.Select((art, i) => _mapper.Map<HistoryDto>((art, articlesLocale[i])));

        return Ok(history);
    }
}