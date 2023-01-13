﻿using AutoMapper;
using Contracts;
using Entities.DTOs;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace Ukranian_Culture.Backend.Controllers;

[Route("api/{cultureId:guid}/[controller]")]
[ApiController]
public class ArticlesLocaleController : ControllerBase
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly IMapper _mapper;
    private readonly ILoggerManager _logger;

    public ArticlesLocaleController(IRepositoryManager repositoryManager, IMapper mapper, ILoggerManager logger)
    {
        _repositoryManager = repositoryManager;
        _mapper = mapper;
        _logger = logger;
    }
    [HttpGet]
    public async Task<IActionResult> GetAllArticlesLocales(Guid cultureId)
    {
        if (await IsCultureExistInDb(cultureId) == false)
            return NotFound(NotFoundCultureMessage(cultureId)); ;

        return Ok(await _repositoryManager.ArticleLocales
            .GetArticlesLocaleByConditionAsync(artL => artL.CultureId == cultureId, ChangesType.AsNoTracking));
    }

    [HttpGet("{id:guid}", Name = "ArticleLocaleById")]
    public async Task<IActionResult> GetArticleLocaleById(Guid id, Guid cultureId)
    {
        if (await IsCultureExistInDb(cultureId) == false)
            return NotFound(NotFoundCultureMessage(cultureId)); ;

        if (await _repositoryManager
                .ArticleLocales
                .GetFirstByConditionAsync(art => art.Id == id && art.CultureId == cultureId, ChangesType.AsNoTracking)
            is { } articleLocale)
        {
            return Ok(articleLocale);
        }

        var message = NotFoundArticleLocaleMessage(id);
        _logger.LogError(message);
        return NotFound(message);
    }

    [HttpPost]
    public async Task<IActionResult> CreateArticleLocale([FromBody] ArticleLocaleToCreateDto? articleLocaleCreateDto, Guid cultureId)
    {
        if (articleLocaleCreateDto is null)
        {
            var message = BadRequestMessage<ArticleLocaleToCreateDto>();
            _logger.LogError(message);
            return BadRequest(message);
        }

        if (await IsCultureExistInDb(cultureId) == false)
            return NotFound(NotFoundCultureMessage(cultureId)); ;

        var articleEntity = _mapper.Map<ArticlesLocale>(articleLocaleCreateDto);
        _repositoryManager.ArticleLocales.CreateArticlesLocaleForCulture(cultureId, articleEntity);
        await _repositoryManager.SaveAsync();

        return Ok(articleEntity);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteArticleLocale(Guid id, Guid cultureId)
    {
        if (await IsCultureExistInDb(cultureId) == false)
            return NotFound(NotFoundCultureMessage(cultureId)); ;

        var articleLocale = await _repositoryManager.ArticleLocales
            .GetFirstByConditionAsync(article => article.Id == id && article.CultureId == cultureId, ChangesType.AsNoTracking);

        if (articleLocale is null)
        {
            var message = NotFoundArticleLocaleMessage(id);
            _logger.LogInfo(message);
            return NotFound(message);
        }

        _repositoryManager.ArticleLocales.DeleteArticlesLocale(articleLocale);
        await _repositoryManager.SaveAsync();
        return NoContent();
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateArticleLocale(Guid id, [FromBody] ArticleLocaleToUpdateDto? articleLocaleToUpdate, Guid cultureId)
    {
        if (articleLocaleToUpdate is null)
        {
            var message = BadRequestMessage<ArticleToUpdateDto>();
            _logger.LogError(message);
            return BadRequest(message);
        }

        if (await IsCultureExistInDb(cultureId) == false)
            return NotFound(NotFoundCultureMessage(cultureId)); ;

        var articleEntity = await _repositoryManager.ArticleLocales
            .GetFirstByConditionAsync(art => art.Id == id && art.CultureId == cultureId, ChangesType.Tracking);

        if (articleEntity is null)
        {
            var message = NotFoundArticleLocaleMessage(id);
            _logger.LogInfo(message);
            return NotFound(message);
        }

        _mapper.Map(articleLocaleToUpdate, articleEntity);
        await _repositoryManager.SaveAsync();
        return NoContent();
    }

    private string NotFoundCultureMessage(Guid cultureId) => $"Culture with this id: \"{cultureId}\" doesn't exist";
    private string NotFoundArticleLocaleMessage(Guid id) => $"ArticleLocale with id: {id} doesn't exist in the database.";
    private string BadRequestMessage<T>() => $"{typeof(T).Name} object sent from client is null.";
    private async Task<bool> IsCultureExistInDb(Guid cultureId)
    {
        var culture = await _repositoryManager.Cultures.GetCultureAsync(cultureId, ChangesType.AsNoTracking);
        if (culture is not null)
            return true;

        _logger.LogError($"Culture with this id: \"{cultureId}\" doesn't exist");
        return false;

    }
}