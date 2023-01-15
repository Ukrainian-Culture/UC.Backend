using AutoMapper;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace Ukranian_Culture.Backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CultureController : ControllerBase
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly IMapper _mapper;
    private readonly ILoggerManager _logger;
    private readonly IErrorMessageProvider _messageProvider;

    public CultureController(IRepositoryManager repositoryManager, IMapper mapper, ILoggerManager logger, IErrorMessageProvider messageProvider)
    {
        _repositoryManager = repositoryManager;
        _mapper = mapper;
        _logger = logger;
        _messageProvider = messageProvider;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCultures()
    {
        var cultures
            = await _repositoryManager
                    .Cultures
                    .GetCulturesByCondition(_ => true, ChangesType.AsNoTracking);

        int counter = 0;
        var cultureDto = cultures.ToDictionary(_ => counter++, culture => culture.Id);
        return Ok(cultureDto);
    }

    [HttpGet("{cultureId:guid}")]
    public async Task<IActionResult> GetCulture(Guid cultureId)
    {
        if (await _repositoryManager.Cultures.GetCultureAsync(cultureId, ChangesType.AsNoTracking)
            is { } culture)
        {
            return Ok(culture);
        }

        var errorMessage = _messageProvider.NotFoundMessage<Culture>(cultureId);
        _logger.LogError(errorMessage);
        return NotFound(errorMessage);
    }
}