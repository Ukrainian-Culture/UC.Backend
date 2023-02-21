using System.Linq.Expressions;
using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repositories;

public class UserHistoryRepository : RepositoryBase<UserHistory>, IUserHistoryRepository
{
    private const int HistoryToGetCount = 12;

    public UserHistoryRepository(RepositoryContext context) : base(context)
    {
    }

    public Task<IEnumerable<UserHistory>> GetAllUserHistoryByConditionAsync(
        Expression<Func<UserHistory, bool>> func, ChangesType changeType)
        => Task.FromResult<IEnumerable<UserHistory>>(Context
            .UsersHistories
            .Where(func)
            .Take(HistoryToGetCount));

    public Task<UserHistory?> GetFirstOrDefaultAsync(Expression<Func<UserHistory, bool>> func, ChangesType changeType)
        => FindByCondition(func, changeType).FirstOrDefaultAsync();

    public void AddHistoryToUser(Guid userId, UserHistory userHistory)
    {
        userHistory.UserId = userId;
        userHistory.Id = Guid.NewGuid();
        Create(userHistory);
    }

    public Task ClearOldHistory()
    {
        var collectionToRemove = Context
            .UsersHistories
            .OrderByDescending(x => x.DateOfWatch)
            .Skip(HistoryToGetCount);

        Context.UsersHistories.RemoveRange(collectionToRemove);
        return Task.CompletedTask;
    }

    public async Task<bool> IsUserContainHistory(Guid userId, string title)
        => await Context
                .UsersHistories
                .FirstOrDefaultAsync(userH => userH.Title == title && userH.UserId == userId)
            is not null;
}