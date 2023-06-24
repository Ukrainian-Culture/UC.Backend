using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Ukranian_Culture.Backend.ActionFilters.ArticleLocaleActionFilters;

public class ArticleLocaleIEmumerableExistAttribute : IAsyncActionFilter
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly ILoggerManager _logger;
    private readonly IErrorMessageProvider _messageProvider;
    private ChangesType _trackChanges;

    public ArticleLocaleIEmumerableExistAttribute(IRepositoryManager repositoryManager,
        ILoggerManager logger,
        IErrorMessageProvider messageProvider)
    {
        _repositoryManager = repositoryManager;
        _logger = logger;
        _messageProvider = messageProvider;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var culture = await GetValue(context);
        if (culture is null) return;

        var articlesLocale =
            await _repositoryManager
                .ArticleLocales
                .GetArticlesLocaleByConditionAsync(artL => artL.CultureId == culture.Id,
                    _trackChanges);
        context.HttpContext.Items.Add("articlesLocale", articlesLocale);
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