using API.Application.Repositories;
using API.Domain.Entities;
using API.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace API.Infrastructure.Repositories;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(ApplicationDbContext context) : base(context)
    {
    }

    public Task<User?> GetUserByEmail(string email, CancellationToken cancellationToken)
    {
        return _context.Users.Where(u => u.Email == email).FirstOrDefaultAsync(cancellationToken);
    }

    public Task<List<User>> GetUserDeleted(CancellationToken cancellationToken)
    {
        return _context.Users.IgnoreQueryFilters().Where(c => c.IsDeleted).ToListAsync(cancellationToken);
    }
}
