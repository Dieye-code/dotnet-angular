using API.Application.Repositories;
using API.Infrastructure.Persistence;

namespace API.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{

    private readonly ApplicationDbContext _context;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }

    public Task Save(CancellationToken cancellationToken)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }
}
