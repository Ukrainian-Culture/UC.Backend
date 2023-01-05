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
        var articles = await _repositoryManager.Articles.GetArticlesByCondition(art => art.Region == region, ChangesType.AsNoTracking);

        var articlesLocale = (await articles
            .Select(art => art.Id)
            .Select( async id => await _repositoryManager.ArticleLocales.GetArticlesLocaleByCondition(artl => artl.Id == id && artl.CultureId == culture, ChangesType.AsNoTracking)).First()).ToList();

        var history = articles.Select((art, i) => _mapper.Map<HistoryDto>((art, articlesLocale[i])));

        return Ok(history);
    }
}