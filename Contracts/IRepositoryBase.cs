using System.Linq.Expressions;

namespace Contracts;

public interface IRepositoryBase<T>
{
    IQueryable<T> FindAll(ChangesType trackChanges);
    IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, ChangesType trackChanges);
    void Create(T entity);
    void Update(T entity);
    void Delete(T entity);
}