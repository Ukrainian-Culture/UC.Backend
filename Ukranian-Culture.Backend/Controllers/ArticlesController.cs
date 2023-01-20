using AutoMapper;
using Contracts;
using Entities.DTOs;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ukranian_Culture.Backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ArticlesController : ControllerBase
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly IMapper _mapper;
    private readonly ILoggerManager _logger;
    private readonly IErrorMessageProvider _messageProvider;
    public ArticlesController(IRepositoryManager repositoryManager, IMapper mapper, ILoggerManager logger, IErrorMessageProvider messageProvider)
    {
        _repositoryManager = repositoryManager;
        _mapper = mapper;
        _logger = logger;
        _messageProvider = messageProvider;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllArticles()
    {
        var articles = await _repositoryManager.Articles.GetAllByConditionAsync(_ => true, ChangesType.AsNoTracking);
        var articlesDtos = _mapper.Map<IEnumerable<ArticleToGetDto>>(articles);
        return Ok(articlesDtos);
    }

    [HttpGet("{id:guid}", Name = "ArticleById")]
    public async Task<IActionResult> GetArticleById(Guid id)
    {
        if (await _repositoryManager.Articles.GetFirstByConditionAsync(art => art.Id == id, ChangesType.AsNoTracking)
            is { } article)
        {
            var articleDto = _mapper.Map<ArticleToGetDto>(article);
            return Ok(articleDto);
        }

        var errorMessage = _messageProvider.NotFoundMessage<Article>(id);
        _logger.LogError(errorMessage);
        return NotFound(errorMessage);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateArticle([FromBody] ArticleToCreateDto? articleCreateDto)
    {
        if (articleCreateDto is null)
        {
            var errorMessage = _messageProvider.BadRequestMessage<ArticleLocaleToCreateDto>();
            _logger.LogError(errorMessage);
            return BadRequest(errorMessage);
        }

        var articleEntity = _mapper.Map<Article>(articleCreateDto);
        _repositoryManager.Articles.CreateArticle(articleEntity);
        await _repositoryManager.SaveAsync();

        return Ok(articleEntity);
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteArticle(Guid id)
    {
        var article = await _repositoryManager.Articles
            .GetFirstByConditionAsync(article => article.Id == id, ChangesType.AsNoTracking);

        if (article is null)
        {
            var errorMessage = _messageProvider.NotFoundMessage<Article>(id);
            _logger.LogInfo(errorMessage);
            return NotFound(errorMessage);
        }

        _repositoryManager.Articles.DeleteArticle(article);
        await _repositoryManager.SaveAsync();
        return NoContent();
    }

    [HttpPut("{id:guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateArticle(Guid id, [FromBody] ArticleToUpdateDto? articleToUpdate)
    {
        if (articleToUpdate is null)
        {
            var errorMessage = _messageProvider.BadRequestMessage<ArticleToUpdateDto>();
            _logger.LogError(errorMessage);
            return BadRequest(errorMessage);
        }

        var articleEntity = await _repositoryManager.Articles
            .GetFirstByConditionAsync(art => art.Id == id, ChangesType.Tracking);

        if (articleEntity is null)
        {
            var errorMessage = _messageProvider.NotFoundMessage<Article>(id);
            _logger.LogInfo(errorMessage);
            return NotFound(errorMessage);
        }

        _mapper.Map(articleToUpdate, articleEntity);
        await _repositoryManager.SaveAsync();
        return NoContent();
    }
}