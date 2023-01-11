using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repositories;

public class UserRepository : RepositoryBase<User>, IUserRepository
{
    public UserRepository(RepositoryContext context)
        : base(context)
    {
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync(ChangesType trackChanges)
        => await FindAll(trackChanges)
            .ToListAsync();

    public async Task<User> GetUserByIdAsync(Guid id, ChangesType trackChanges)
        => await FindByCondition(user => user.Id == id, trackChanges)
            .SingleAsync();


    public void CreateUser(User user) => Create(user);
    public void UpdateUser(User user) => Update(user);
    public void DeleteUser(User user) => Delete(user);
}