using Contracts;
using Entities;
using Entities.Models;

namespace Repositories;

public class InfoRepository : RepositoryBase<Info>, IInfoRepository
{
    public InfoRepository(RepositoryContext context)
        : base(context)
    {
    }
}