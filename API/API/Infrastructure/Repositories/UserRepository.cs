using API.Application.Repositories;
using API.Domain.Entities;
using API.Infrastructure.Persistence;

namespace API.Infrastructure.Repositories;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(ApplicationDbContext context) : base(context)
    {
    }
}
