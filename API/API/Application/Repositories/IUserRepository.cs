using API.Domain.Entities;

namespace API.Application.Repositories;

public interface IUserRepository : IBaseRepository<User>
{
    Task<List<User>> GetUserDeleted(CancellationToken cancellationToken);
    Task<User?> GetUserByEmail(string email, CancellationToken cancellationToken);
}
