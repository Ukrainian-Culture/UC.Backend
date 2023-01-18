using Entities.Models;

namespace Contracts;

public interface IUserRepository
{
    public Task<IEnumerable<User>> GetAllUsersAsync(ChangesType trackChanges);
    public Task<User?> GetUserByIdAsync(Guid id, ChangesType trackChanges);
    void CreateUser(User company);
    void UpdateUser(User company);
    void DeleteUser(User company);
}