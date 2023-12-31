﻿using API.Application.Repositories;
using API.Domain.Entities;
using API.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace API.Infrastructure.Repositories;

public class BaseRepository<T> : IBaseRepository<T> where T : EntityBase
{
    protected readonly ApplicationDbContext _context;

    public BaseRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public void Create(T entity)
    {
        _context.Add(entity);
    }

    public void Delete(T entity)
    {
        _context.Remove(entity);
    }

    public virtual Task<List<T>> FindByQuery(Expression<Func<T, bool>> predicate , CancellationToken cancellationToken)
    {
        return _context.Set<T>().Where(predicate).ToListAsync(cancellationToken);
    }

    public virtual Task<T> Get(Guid id, CancellationToken cancellationToken)
    {
        return _context.Set<T>().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public virtual  Task<List<T>> GetAll(CancellationToken cancellationToken)
    {
        return _context.Set<T>().ToListAsync(cancellationToken);
    }

    public void Update(T entity)
    {
        _context.Update<T>(entity);
    }
}



