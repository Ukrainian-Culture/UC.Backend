using Contracts;
using Entities;
using Entities.Models;

namespace Repositories;

public class ArticleLocalesRepository : IArticleLocalesRepository// RepositoryBase<ArticlesLocale>
{
    public ArticleLocalesRepository(RepositoryContext context)
        //: base(context)
    {
    }
}