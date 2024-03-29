﻿using AutoMapper;
using Contracts;
using Entities.DTOs;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using PdfSharp.Drawing;
using PdfSharp.Fonts;
using PdfSharp.Pdf;
using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
using FontResolver;
using Ukranian_Culture.Backend.ActionFilters.ArticleLocaleActionFilters;

namespace Ukranian_Culture.Backend.Controllers;

[Route("api/{cultureId:guid}/[controller]")]
[ApiController]
public class ArticlesLocaleController : ControllerBase
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly IMapper _mapper;
    private readonly ILoggerManager _logger;
    private readonly IErrorMessageProvider _messageProvider;

    public ArticlesLocaleController(IRepositoryManager repositoryManager, IMapper mapper, ILoggerManager logger,
        IErrorMessageProvider messageProvider)
    {
        _repositoryManager = repositoryManager;
        _mapper = mapper;
        _logger = logger;
        _messageProvider = messageProvider;
    }

    [HttpGet]
    [ServiceFilter(typeof(ArticleLocaleIEmumerableExistAttribute))]
    public Task<IActionResult> GetAllArticlesLocales(Guid cultureId)
    {
        var articlesLocale = HttpContext.Items["articlesLocale"] as IEnumerable<ArticlesLocale>;

        var articlesLocaleDtos
            = articlesLocale
                .Select(art => GetArticleLocaleDtoWithFullInfo(art).Result)
                .ToList();
        return Task.FromResult<IActionResult>(Ok(articlesLocaleDtos));
    }

    [HttpGet("{id:guid}", Name = "ArticleLocaleById")]
    [ServiceFilter(typeof(ArticleLocaleExistAttribute))]
    public async Task<IActionResult> GetArticleLocaleById(Guid id, Guid cultureId)
    {
        var articleLocale = HttpContext.Items["articleLocale"] as ArticlesLocale;
        return Ok(await GetArticleLocaleDtoWithFullInfo(articleLocale!));
    }

    private async Task<ArticlesLocaleToGetDto> GetArticleLocaleDtoWithFullInfo(ArticlesLocale articleLocale)
    {
        var article = await _repositoryManager
            .Articles
            .GetFirstByConditionAsync(art => art.Id == articleLocale.Id, ChangesType.AsNoTracking);
        var articleLocaleDto = _mapper.Map<ArticlesLocaleToGetDto>(articleLocale);
        articleLocaleDto.Region = article?.Region;
        articleLocaleDto.Category = article?.Category?.Name;
        return articleLocaleDto;
    }

    [HttpPost]
    public async Task<IActionResult> CreateArticleLocale([FromBody] ArticleLocaleToCreateDto? articleLocaleCreateDto,
        Guid cultureId)
    {
        if (articleLocaleCreateDto is null)
        {
            var message = _messageProvider.BadRequestMessage<ArticleLocaleToCreateDto>();
            _logger.LogError(message);
            return BadRequest(message);
        }

        if (await IsCultureExistInDb(cultureId) == false)
            return NotFound(_messageProvider.NotFoundMessage<Culture, Guid>(cultureId));

        var articleEntity = _mapper.Map<ArticlesLocale>(articleLocaleCreateDto);
        _repositoryManager.ArticleLocales.CreateArticlesLocaleForCulture(cultureId, articleEntity);
        await _repositoryManager.SaveAsync();

        return Ok(articleEntity);
    }

    [HttpDelete("{id:guid}")]
    [ServiceFilter(typeof(ArticleLocaleExistAttribute))]
    public async Task<IActionResult> DeleteArticleLocale(Guid id, Guid cultureId)
    {
        var articleEntity = HttpContext.Items["articleLocale"] as ArticlesLocale;
        _repositoryManager.ArticleLocales.DeleteArticlesLocale(articleEntity!);
        await _repositoryManager.SaveAsync();
        return NoContent();
    }

    [HttpPut("{id:guid}")]
    [ServiceFilter(typeof(ArticleLocaleExistAttribute))]
    public async Task<IActionResult> UpdateArticleLocale(Guid id,
        [FromBody] ArticleLocaleToUpdateDto? articleLocaleToUpdate, Guid cultureId)
    {
        if (articleLocaleToUpdate is null)
        {
            var message = _messageProvider.BadRequestMessage<ArticleToUpdateDto>();
            _logger.LogError(message);
            return BadRequest(message);
        }

        var articleEntity = HttpContext.Items["articleLocale"] as ArticlesLocale;
        _mapper.Map(articleLocaleToUpdate, articleEntity);
        await _repositoryManager.SaveAsync();
        return NoContent();
    }


    private async Task<bool> IsCultureExistInDb(Guid cultureId)
    {
        var culture =
            await _repositoryManager
                .Cultures
                .GetFirstByConditionAsync(cul => cul.Id == cultureId, ChangesType.AsNoTracking);
        if (culture is not null)
            return true;

        _logger.LogError(_messageProvider.NotFoundMessage<Culture, Guid>(cultureId));
        return false;
    }

    [HttpGet("ArticleLocalePDFById")]
    [ServiceFilter(typeof(ArticleLocaleExistAttribute))]
    public Task<IActionResult> GetArticleLocalePdfById(Guid id, Guid cultureId)
    {
        var articleLocale = HttpContext.Items["articleLocale"] as ArticlesLocale;

        Document document = new Document();
        Section section = document.AddSection();

        Paragraph header = section.AddParagraph();
        header.Format.Alignment = ParagraphAlignment.Center;
        header.Format.Font.Name = "MacPaw";
        header.Format.Font.Bold = true;
        header.Format.Font.Size = 15;
        header.Format.SpaceAfter = 12;
        header.AddText(articleLocale.Title);

        Paragraph paragraph = section.AddParagraph();
        paragraph.Format.Alignment = ParagraphAlignment.Left;
        paragraph.Format.Font.Name = "MacPaw";
        paragraph.Format.Font.Size = 12;
        paragraph.AddText(articleLocale.Content);

        PdfDocumentRenderer pdfRenderer = new PdfDocumentRenderer(true);
        pdfRenderer.Document = document;
        pdfRenderer.RenderDocument();
        byte[]? response = null;
        MemoryStream ms = new MemoryStream();
        pdfRenderer.PdfDocument.Save(ms);
        string filename = articleLocale.Title + ".pdf";
        return Task.FromResult<IActionResult>(File(ms.ToArray(), "application/pdf", filename));
    }
}