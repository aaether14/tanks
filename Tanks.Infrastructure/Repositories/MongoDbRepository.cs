using System;
using System.Threading.Tasks;
using MongoDB.Driver;
using Tanks.Application.Repositories;
using Tanks.Domain.DomainModels;

namespace Tanks.Infrastructure.Repositories;

public class MongoDbRepository<T, TId> : IRepository<T, TId>
    where T : IEntity<TId>
{

    private readonly IMongoCollection<T> _collection;

    public MongoDbRepository(IMongoCollection<T> collection)
    {
        _collection = collection;
    }

    public Task AddAsync(T item)
    {
        return _collection.InsertOneAsync(item);
    }

    public async Task<T> GetByIdAsync(TId id)
    {
        return await GetByIdOrDefaultAsync(id) 
            ?? throw new Exception("replace!");
    }

    public async Task<T?> GetByIdOrDefaultAsync(TId id)
    {
        var filter = Builders<T>.Filter.Eq(entity => entity.Id, id);

        T? item = await (await _collection.FindAsync(filter)).FirstOrDefaultAsync();

        return item;
    }
    
}
