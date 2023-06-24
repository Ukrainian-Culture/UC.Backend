using System.Linq.Expressions;
using Contracts;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace Repositories;

public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
{
    protected readonly RepositoryContext Context;

    protected RepositoryBase(RepositoryContext context)
    {
        Context = context;
    }

    public IQueryable<T> FindAll(ChangesType trackChanges)
        => trackChanges switch
        {
            ChangesType.Tracking => Context.Set<T>(),
            ChangesType.AsNoTracking => Context.Set<T>().AsNoTracking(),
            _ => throw new ArgumentOutOfRangeException(nameof(trackChanges), trackChanges, null)
        };


    public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, ChangesType trackChanges)
        => trackChanges switch
        {
            ChangesType.Tracking => Context.Set<T>().Where(expression),
            ChangesType.AsNoTracking => Context.Set<T>().AsNoTracking().Where(expression),
            _ => throw new ArgumentOutOfRangeException(nameof(trackChanges), trackChanges, null)
        };

    public void Create(T entity) => Context.Set<T>().Add(entity);
    public void Update(T entity) => Context.Set<T>().Update(entity);
    public void Delete(T entity) => Context.Set<T>().Remove(entity);

}