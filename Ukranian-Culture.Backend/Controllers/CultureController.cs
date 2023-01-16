using AutoMapper;
using Contracts;
using Entities.DTOs;
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
        var cultures = await _repositoryManager.Cultures.GetCulturesByCondition(_ => true, ChangesType.AsNoTracking);
        var culturesDtos = _mapper.Map<IEnumerable<CultureToGetDto>>(cultures);
        return Ok(culturesDtos);
    }

    [HttpGet("{cultureId:guid}")]
    public async Task<IActionResult> GetCulture(Guid cultureId)
    {
        if (await _repositoryManager.Cultures.GetCultureAsync(cultureId, ChangesType.AsNoTracking)
            is { } culture)
        {
            var cultureDto = _mapper.Map<CultureToGetDto>(culture);
            return Ok(cultureDto);
        }

        var errorMessage = _messageProvider.NotFoundMessage<Culture>(cultureId);
        _logger.LogError(errorMessage);
        return NotFound(errorMessage);
    }

    [HttpGet("ids")]
    public async Task<IActionResult> GetAllCulturesIds()
    {
        var cultures
            = await _repositoryManager
                .Cultures
                .GetCulturesByCondition(_ => true, ChangesType.AsNoTracking);

        Dictionary<string, Guid> cultureIds = cultures.ToDictionary(cult => cult.Name, culture => culture.Id);
        return Ok(cultureIds);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCulture([FromBody] CultureToCreateDto? cultureCreateDto)
    {
        if (cultureCreateDto is null)
        {
            var errorMessage = _messageProvider.BadRequestMessage<CultureToCreateDto>();
            _logger.LogError(errorMessage);
            return BadRequest(errorMessage);
        }

        var cultureEntity = _mapper.Map<Culture>(cultureCreateDto);
        _repositoryManager.Cultures.CreateCulture(cultureEntity);
        await _repositoryManager.SaveAsync();

        return Ok(_mapper.Map<CultureToGetDto>(cultureEntity));
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteCulture(Guid id)
    {
        var culture = await _repositoryManager
            .Cultures
            .GetCultureAsync(id, ChangesType.AsNoTracking);

        if (culture is null)
        {
            var errorMessage = _messageProvider.NotFoundMessage<Culture>(id);
            _logger.LogInfo(errorMessage);
            return NotFound(errorMessage);
        }

        _repositoryManager.Cultures.DeleteCulture(culture);
        await _repositoryManager.SaveAsync();
        return NoContent();
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateCulture(Guid id, [FromBody] CultureToUpdateDto? cultureToUpdate)
    {
        if (cultureToUpdate is null)
        {
            var errorMessage = _messageProvider.BadRequestMessage<CultureToUpdateDto>();
            _logger.LogError(errorMessage);
            return BadRequest(errorMessage);
        }

        var cultureEntity = await _repositoryManager
            .Cultures
            .GetCultureAsync(id, ChangesType.Tracking);

        if (cultureEntity is null)
        {
            var errorMessage = _messageProvider.NotFoundMessage<Culture>(id);
            _logger.LogInfo(errorMessage);
            return NotFound(errorMessage);
        }

        _mapper.Map(cultureToUpdate, cultureEntity);
        await _repositoryManager.SaveAsync();
        return NoContent();
    }
}