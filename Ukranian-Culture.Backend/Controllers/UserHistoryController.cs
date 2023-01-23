using AutoMapper;
using Contracts;
using Entities.DTOs;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace Ukranian_Culture.Backend.Controllers;

[Route("api/{userId:guid}/[controller]")]
[ApiController]
public class UserHistoryController : ControllerBase
{
    // api/idUser/UserHistory/new UserHistory{Title, ArticleId, UserId}

    private readonly IRepositoryManager _repository;
    private readonly IMapper _mapper;
    private readonly ILoggerManager _logger;
    private readonly IErrorMessageProvider _messageProvider;
    public UserHistoryController(IRepositoryManager repository, IMapper mapper, ILoggerManager logger, IErrorMessageProvider messageProvider)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
        _messageProvider = messageProvider;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUserHistory(Guid userId)
    {
        User? user = await _repository.Users.GetUserByIdAsync(userId, ChangesType.AsNoTracking);
        if (user is null)
        {
            var message = _messageProvider.NotFoundMessage<User>(userId);
            _logger.LogError(message);
            return NotFound(message);
        }

        var history = await _repository
            .UserHistory
            .GetAllUserHistoryByConditionAsync(his => his.UserId == userId, ChangesType.AsNoTracking);
        return Ok(history);
    }

    [HttpPost]
    public async Task<IActionResult> AddHistoryToUser(Guid userId, HistoryToCreateDto? historyToCreate)
    {
        if (historyToCreate is null)
        {
            var message = _messageProvider.BadRequestMessage<HistoryToCreateDto>();
            _logger.LogError(message);
            return BadRequest(message);
        }

        User? user = await _repository.Users.GetUserByIdAsync(userId, ChangesType.AsNoTracking);
        if (user is null)
        {
            var message = _messageProvider.NotFoundMessage<User>(userId);
            _logger.LogError(message);
            return NotFound(message);
        }

        UserHistory? articleEntity = _mapper.Map<UserHistory>(historyToCreate);
        _repository.UserHistory.AddHistoryToUser(userId, articleEntity);
        await _repository.SaveAsync();
        return NoContent();
    }
}