using System.Linq.Expressions;
using Entities.Models;

namespace Contracts;

public interface IUserRepository
{
    public Task<IEnumerable<User>> GetAllUsersAsync(ChangesType trackChanges);
    public Task<User?> GetFirstByConditionAsync(Expression<Func<User, bool>> func, ChangesType trackChanges);
    void CreateUser(User company);
    void UpdateUser(User company);
    void DeleteUser(User company);
    Task<int> CountAsync { get; }
}