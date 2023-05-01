using System;
using System.Threading.Tasks;
using Tanks.Domain.DomainModels;

namespace Tanks.Application.Repositories;

public interface IRepository<T, TId>
    where T : IEntity<TId>
{

    Task<T> GetByIdAsync(TId id);
    Task<T?> GetByIdOrDefaultAsync(TId id);
    Task AddAsync(T item);

}