using System.Linq.Expressions;
using Entities.Models;

namespace Contracts;

public interface IUserHistoryRepository
{
    Task<IEnumerable<UserHistory>> GetAllUserHistoryByConditionAsync(Expression<Func<UserHistory, bool>> func,
        ChangesType changeType);

    void AddHistoryToUser(Guid userId, UserHistory userHistory);
    Task ClearOldHistory();
}