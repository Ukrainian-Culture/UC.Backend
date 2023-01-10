﻿namespace Contracts;

public interface IRepositoryManager
{
    IUserRepository Users { get; }
    IArticleLocalesRepository ArticleLocales { get; }
    IArticleRepository Articles { get; }
    ICategoryLocalesRepository CategoryLocales { get; }
    ICategoryRepository Categories { get; }
    ICultureRepository Cultures { get; }
    Task SaveAsync();
}