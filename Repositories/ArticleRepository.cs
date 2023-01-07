using Contracts;
using Entities;
using Entities.Models;

namespace Repositories;

public class ArticleRepository : IArticleRepository//RepositoryBase<Article>,
{
    public ArticleRepository(RepositoryContext context)
        //: base(context)
    {
    }
}