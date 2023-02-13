using AutoMapper;
using Contracts;
using Entities.DTOs;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using PdfSharpCore;
using PdfSharpCore.Pdf;
using VetCV.HtmlRendererCore.PdfSharpCore;

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
    public async Task<IActionResult> GetAllArticlesLocales(Guid cultureId)
    {
        if (await IsCultureExistInDb(cultureId) == false)
            return NotFound(_messageProvider.NotFoundMessage<Culture>(cultureId));


        var articlesLocale = await _repositoryManager.ArticleLocales
            .GetArticlesLocaleByConditionAsync(artL => artL.CultureId == cultureId, ChangesType.AsNoTracking);
        var articlesLocaleDtos = _mapper.Map<IEnumerable<ArticlesLocaleToGetDto>>(articlesLocale);
        return Ok(articlesLocaleDtos);
    }

    [HttpGet("{id:guid}", Name = "ArticleLocaleById")]
    public async Task<IActionResult> GetArticleLocaleById(Guid id, Guid cultureId)
    {
        if (await IsCultureExistInDb(cultureId) == false)
            return NotFound(_messageProvider.NotFoundMessage<Culture>(cultureId));

        if (await _repositoryManager
                .ArticleLocales
                .GetFirstByConditionAsync(art => art.Id == id && art.CultureId == cultureId, ChangesType.AsNoTracking)
            is { } articleLocale)
        {
            var articleLocaleDto = _mapper.Map<ArticlesLocaleToGetDto>(articleLocale);
            return Ok(articleLocaleDto);
        }

        var message = _messageProvider.NotFoundMessage<ArticlesLocale>(id);
        _logger.LogError(message);
        return NotFound(message);
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
            return NotFound(_messageProvider.NotFoundMessage<Culture>(cultureId));


        var articleEntity = _mapper.Map<ArticlesLocale>(articleLocaleCreateDto);
        _repositoryManager.ArticleLocales.CreateArticlesLocaleForCulture(cultureId, articleEntity);
        await _repositoryManager.SaveAsync();

        return Ok(articleEntity);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteArticleLocale(Guid id, Guid cultureId)
    {
        if (await IsCultureExistInDb(cultureId) == false)
            return NotFound(_messageProvider.NotFoundMessage<Culture>(cultureId));

        var articleLocale = await _repositoryManager.ArticleLocales
            .GetFirstByConditionAsync(article => article.Id == id && article.CultureId == cultureId,
                ChangesType.AsNoTracking);

        if (articleLocale is null)
        {
            var message = _messageProvider.NotFoundMessage<ArticlesLocale>(id);
            _logger.LogInfo(message);
            return NotFound(message);
        }

        _repositoryManager.ArticleLocales.DeleteArticlesLocale(articleLocale);
        await _repositoryManager.SaveAsync();
        return NoContent();
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateArticleLocale(Guid id,
        [FromBody] ArticleLocaleToUpdateDto? articleLocaleToUpdate, Guid cultureId)
    {
        if (articleLocaleToUpdate is null)
        {
            var message = _messageProvider.BadRequestMessage<ArticleToUpdateDto>();
            _logger.LogError(message);
            return BadRequest(message);
        }

        if (await IsCultureExistInDb(cultureId) == false)
            return NotFound(_messageProvider.NotFoundMessage<Culture>(cultureId));
        ;

        var articleEntity = await _repositoryManager.ArticleLocales
            .GetFirstByConditionAsync(art => art.Id == id && art.CultureId == cultureId, ChangesType.Tracking);

        if (articleEntity is null)
        {
            var message = _messageProvider.NotFoundMessage<ArticlesLocale>(id);
            _logger.LogInfo(message);
            return NotFound(message);
        }

        _mapper.Map(articleLocaleToUpdate, articleEntity);
        await _repositoryManager.SaveAsync();
        return NoContent();
    }


    private async Task<bool> IsCultureExistInDb(Guid cultureId)
    {
        var culture = await _repositoryManager
            .Cultures
            .GetFirstByConditionAsync(culture1 => culture1.Id == cultureId, ChangesType.AsNoTracking);
        if (culture is not null) return true;

        _logger.LogError(_messageProvider.NotFoundMessage<Culture>(cultureId));
        return false;
    }

    [HttpGet("ArticleLocalePDFById")]
    public async Task<IActionResult> GetArticleLocalePDFById(Guid id, Guid cultureId)
    {
        if (await IsCultureExistInDb(cultureId) == false)
            return NotFound(_messageProvider.NotFoundMessage<Culture>(cultureId));
        ;

        if (await _repositoryManager
                .ArticleLocales
                .GetFirstByConditionAsync(art => art.Id == id && art.CultureId == cultureId, ChangesType.AsNoTracking)
            is { } articleLocale)
        {
            var document = new PdfDocument();
            PdfGenerator.AddPdfPages(document, articleLocale.Content, PageSize.A4, 20, null, null, null);
            byte[]? response = null;
            MemoryStream ms = new MemoryStream();
            document.Save(ms);
            response = ms.ToArray();
            string filename = articleLocale.Title + ".pdf";
            return File(response, "application/pdf", filename);
        }

        var message = _messageProvider.NotFoundMessage<ArticlesLocale>(id);
        _logger.LogError(message);
        return NotFound(message);
    }
}