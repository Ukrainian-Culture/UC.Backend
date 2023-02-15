﻿using AutoMapper;
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
        User? user = await _repository.Users.GetFirstByConditionAsync(user => user.Email == email, ChangesType.AsNoTracking);
        if (user is null)
        {
            var message = _messageProvider.NotFoundMessage<User, string>(email);
            _logger.LogError(message);
            return NotFound(message);
        }

        var history = await _repository
            .UserHistory
            .GetAllUserHistoryByConditionAsync(his => his.UserId == user.Id, ChangesType.AsNoTracking);
        return Ok(history);
    }

    [HttpPost]
    public async Task<IActionResult> AddHistoryToUser(string email, HistoryToCreateDto? historyToCreate)
    {
        if (historyToCreate is null)
        {
            var message = _messageProvider.BadRequestMessage<HistoryToCreateDto>();
            _logger.LogError(message);
            return BadRequest(message);
        }

        User? user = await _repository.Users.GetFirstByConditionAsync(user => user.Email == email, ChangesType.AsNoTracking);
        if (user is null)
        {
            var message = _messageProvider.NotFoundMessage<User, string>(email);
            _logger.LogError(message);
            return NotFound(message);
        }

        UserHistory? articleEntity = _mapper.Map<UserHistory>(historyToCreate);
        _repository.UserHistory.AddHistoryToUser(user.Id, articleEntity);
        await _repository.SaveAsync();
        return NoContent();
    }
}