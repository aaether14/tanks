using System;
using System.Threading.Tasks;
using MongoDB.Driver;
using Tanks.Application.Repositories;
using Tanks.Domain.DomainModels;

namespace Tanks.Infrastructure.Repositories;

public class MongoDbTankRepository : ITankRepository
{

    public const string CollectionName = "tanks";

    private readonly IMongoCollection<Tank> _tankCollection;

    public MongoDbTankRepository(IMongoCollection<Tank> tankCollection)
    {
        _tankCollection = tankCollection;
    }

    public Task AddTankAsync(Tank tank)
    {
        return _tankCollection.InsertOneAsync(tank);
    }

    public async Task<Tank> GetTankByIdAsync(Guid id)
    {
        var filter = Builders<Tank>.Filter.Eq(entity => entity.Id, id);
        return await (await _tankCollection.FindAsync(filter)).FirstAsync();
    }
}