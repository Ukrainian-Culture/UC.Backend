using System.Linq.Expressions;
using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repositories;

public class UserHistoryRepository : RepositoryBase<UserHistory>, IUserHistoryRepository
{
    private const int HistoryToGetCount = 5;

    public UserHistoryRepository(RepositoryContext context) : base(context)
    {
    }

    public async Task<IEnumerable<UserHistory>> GetAllUserHistoryByConditionAsync(
        Expression<Func<UserHistory, bool>> func, ChangesType changeType)
        => Context
            .UsersHistories
            .Where(func)
            .Take(HistoryToGetCount);
}