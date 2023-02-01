using System.Linq.Expressions;
using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repositories;

public class UserHistoryRepository : RepositoryBase<UserHistory>, IUserHistoryRepository
{
    private const int HistoryToGetCount = 10;

    public UserHistoryRepository(RepositoryContext context) : base(context)
    {
    }

    public async Task<IEnumerable<UserHistory>> GetAllUserHistoryByConditionAsync(
        Expression<Func<UserHistory, bool>> func, ChangesType changeType)
        => Context
            .UsersHistories
            .Where(func)
            .Take(HistoryToGetCount);

    public void AddHistoryToUser(Guid userId, UserHistory userHistory)
    {
        userHistory.UserId = userId;
        userHistory.Id = Guid.NewGuid();
        Create(userHistory);
    }
}