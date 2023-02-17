using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Ukranian_Culture.Backend.ActionFilters.ArticleLocaleActionFilters;

public class ArticleLocaleExistAttribute : IAsyncActionFilter
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly ILoggerManager _logger;
    private readonly IErrorMessageProvider _messageProvider;
    private ChangesType _trackChanges;

    public ArticleLocaleExistAttribute(IRepositoryManager repositoryManager, IErrorMessageProvider messageProvider,
        ILoggerManager logger)
    {
        _repositoryManager = repositoryManager;
        _messageProvider = messageProvider;
        _logger = logger;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var culture = await GetValue(context);
        if (culture is null) return;

        var id = (Guid)context.ActionArguments["id"]!;
        var article =
            await _repositoryManager
                .ArticleLocales
                .GetFirstByConditionAsync(art => art.Id == id && art.CultureId == culture.Id,
                    _trackChanges);

        if (article is null)
        {
            var message = _messageProvider.NotFoundMessage<ArticlesLocale, Guid>(id);
            _logger.LogInfo(message);
            context.Result = new NotFoundObjectResult(message);
            return;
        }

        context.HttpContext.Items.Add("articleLocale", article);
        await next();
    }

    private async Task<Culture?> GetValue(ActionExecutingContext context)
    {
        _trackChanges = context.HttpContext.Request.Method.Equals("PUT")
            ? ChangesType.Tracking
            : ChangesType.AsNoTracking;

        var cultureId = (Guid)context.ActionArguments["cultureId"]!;
        var culture = await _repositoryManager
            .Cultures
            .GetFirstByConditionAsync(cul => cul.Id == cultureId, _trackChanges);

        if (culture is not null) return culture;

        var message = _messageProvider.NotFoundMessage<Culture, Guid>(cultureId);
        _logger.LogError(message);
        context.Result = new NotFoundObjectResult(message);
        return null;
    }
}