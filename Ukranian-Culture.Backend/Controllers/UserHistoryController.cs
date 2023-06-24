using AutoMapper;
using Contracts;
using Entities.DTOs;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace Ukranian_Culture.Backend.Controllers;

[Route("api/{email}/[controller]")]
[ApiController]
public class UserHistoryController : ControllerBase
{
    private readonly IRepositoryManager _repository;
    private readonly IMapper _mapper;
    private readonly ILoggerManager _logger;
    private readonly IErrorMessageProvider _messageProvider;

    public UserHistoryController(IRepositoryManager repository, IMapper mapper, ILoggerManager logger,
        IErrorMessageProvider messageProvider)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
        _messageProvider = messageProvider;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUserHistory(string email)
    {
        User? user =
            await _repository.Users.GetFirstByConditionAsync(user => user.Email == email, ChangesType.AsNoTracking);
        if (user is null)
        {
            var message = _messageProvider.NotFoundMessage<User, string>(email);
            _logger.LogError(message);
            return NotFound(message);
        }

        var history = await _repository
            .UserHistory
            .GetAllUserHistoryByConditionAsync(his => his.UserId == user.Id, ChangesType.AsNoTracking);

        var userHistoryDto
            = _mapper.Map<IEnumerable<UserHistoryToGetDto>>(history
                .OrderByDescending(x => x.DateOfWatch));

        return Ok(userHistoryDto);
    }

    [HttpPost]
    public async Task<IActionResult> AddHistoryToUser(string email, HistoryToCreateDto? historyToCreateDto)
    {
        if (historyToCreateDto is null)
        {
            var message = _messageProvider.BadRequestMessage<HistoryToCreateDto>();
            _logger.LogError(message);
            return BadRequest(message);
        }

        User? user =
            await _repository.Users.GetFirstByConditionAsync(user => user.Email == email, ChangesType.AsNoTracking);
        if (user is null)
        {
            var message = _messageProvider.NotFoundMessage<User, string>(email);
            _logger.LogError(message);
            return NotFound(message);
        }

        if (await _repository
                .UserHistory
                .IsUserContainHistory(user.Id, historyToCreateDto.Title))
        {
            var history = await _repository
                .UserHistory
                .GetFirstOrDefaultAsync(his => his.Title == historyToCreateDto.Title &&
                                               his.Region == historyToCreateDto.Region, ChangesType.Tracking);

            _mapper.Map(historyToCreateDto, history);
            await _repository.SaveAsync();
            return NoContent();
        }

        var userHistoryEntity = _mapper.Map<UserHistory>(historyToCreateDto);

        _repository.UserHistory.AddHistoryToUser(user.Id, userHistoryEntity);
        await _repository.UserHistory.ClearOldHistory();
        await _repository.SaveAsync();
        return NoContent();
    }
}