using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repositories;

public class ArticleLocalesRepository : RepositoryBase<ArticlesLocale>, IArticleLocalesRepository
{
    public ArticleLocalesRepository(RepositoryContext context)
        : base(context)
    {
    }

    public async Task<ArticlesLocale> GetArticlesLocaleByIdAsync(int id, ChangesType trackChanges)
       => await FindByCondition(art => art.Id == id, trackChanges)
           .SingleAsync();

    public IEnumerable<ArticlesLocale> GetAllArticlesLocale(ChangesType trackChanges)
        => FindAll(trackChanges).ToList();

    public void CreateArticlesLocale(ArticlesLocale articleL) => Create(articleL);
    public void UpdateArticlesLocale(ArticlesLocale articleL) => Update(articleL);
    public void DeleteArticlesLocale(ArticlesLocale articleL) => Delete(articleL);
}