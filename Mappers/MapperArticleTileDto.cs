using Entities.DTOs;
using Entities.Models;

namespace Mappers;

public class MapperArticleTileDto : Mapper<Article, ArticlesLocale, ArticleTileDto>
{
    protected override int GetId(ArticlesLocale second) => second.Id;

    protected override int GetId(Article first) => first.Id;

    protected override Task<Func<Article, ArticlesLocale, ArticleTileDto>> CreateDto(object[] helpers)
    {
        var categories = (Dictionary<int, string>)helpers[0];

        return Task.FromResult<Func<Article, ArticlesLocale, ArticleTileDto>>((article, articleLocale) => new ArticleTileDto
        {
            ArticleId = article.Id,
            Type = article.Type,
            Region = article.Region,
            SubText = articleLocale.SubText,
            Title = articleLocale.Title,
            Category = categories?[article.CategoryId]
        });
    }
}