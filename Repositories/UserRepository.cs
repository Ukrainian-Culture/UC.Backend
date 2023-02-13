using System.Linq.Expressions;
using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

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

    public async Task<User?> GetFirstByConditionAsync(Expression<Func<User, bool>> func, ChangesType trackChanges)
        => await FindByCondition(func, trackChanges).FirstOrDefaultAsync();

    public void CreateUser(User user) => Create(user);
    public void UpdateUser(User user) => Update(user);
    public void DeleteUser(User user) => Delete(user);
    public Task<int> CountAsync => Context.Users.CountAsync();
}