using API.Domain.Entities;
using System.Linq.Expressions;

namespace API.Application.Repositories;

public interface IBaseRepository<T> where T : EntityBase
{

    void Create(T entity);
    void Update(T entity);
    void Delete(T entity);
    Task<T> Get(Guid id, CancellationToken cancellationToken);
    Task<List<T>> GetAll(CancellationToken cancellationToken);
    Task<List<T>> FindByQuery(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);

}
