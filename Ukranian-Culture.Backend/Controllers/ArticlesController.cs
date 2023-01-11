using AutoMapper;
using Contracts;
using Entities.DTOs;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace Ukranian_Culture.Backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ArticlesController : ControllerBase
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly IMapper _mapper;
    private readonly ILoggerManager _logger;
    public ArticlesController(IRepositoryManager repositoryManager, IMapper mapper, ILoggerManager logger)
    {
        _repositoryManager = repositoryManager;
        _mapper = mapper;
        _logger = logger;
    }


    [HttpGet]
    public async Task<IActionResult> GetAllArticles()
    {
        return Ok(await _repositoryManager.Articles.GetAllByConditionAsync(_ => true, ChangesType.AsNoTracking));
    }

    [HttpGet("{id:int}", Name = "ArticleById")]
    public async Task<IActionResult> GetArticleById(int id)
    {
        if (await _repositoryManager.Articles.GetFirstByConditionAsync(art => art.Id == id, ChangesType.AsNoTracking)
            is { } article)
        {
            return Ok(article);
        }

        _logger.LogError($"Article with id : \"{id}\" no contain in db");
        return NotFound($"Article with id : \"{id}\" no contain in db");
    }

    [HttpPost]
    public async Task<IActionResult> CreateArticle([FromBody] ArticleToCreateDto? articleCreateDto)
    {
        if (articleCreateDto is null)
        {
            _logger.LogError("articleCreateDto object sent from client is null.");
            return BadRequest("articleCreateDto object is null");
        }

        var articleEntity = _mapper.Map<Article>(articleCreateDto);
        _repositoryManager.Articles.CreateArticle(articleEntity);
        await _repositoryManager.SaveAsync();

        return Ok(articleEntity);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteArticle(int id)
    {
        var article = await _repositoryManager.Articles
            .GetFirstByConditionAsync(article => article.Id == id, ChangesType.AsNoTracking);

        if (article is null)
        {
            _logger.LogInfo($"Article with id: {id} doesn't exist in the database.");
            return NotFound($"Article with id : \"{id}\" no contain in db");
        }

        _repositoryManager.Articles.DeleteArticle(article);
        await _repositoryManager.SaveAsync();
        return NoContent();
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateArticle(int id, [FromBody] ArticleToUpdateDto? articleToUpdate)
    {
        if (articleToUpdate is null)
        {
            _logger.LogError("articleToUpdate object sent from client is null.");
            return BadRequest("articleToUpdate object is null");
        }

        var articleEntity = await _repositoryManager.Articles
            .GetFirstByConditionAsync(art => art.Id == id, ChangesType.Tracking);

        if (articleEntity is null)
        {
            _logger.LogInfo($"Article with id: {id} doesn't exist in the database.");
            return NotFound($"Article with id : \"{id}\" no contain in db");
        }

        _mapper.Map(articleToUpdate, articleEntity);
        await _repositoryManager.SaveAsync();
        return NoContent();
    }
}