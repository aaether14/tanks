using System;
using System.Threading.Tasks;
using MongoDB.Driver;
using Tanks.Application.Repositories;
using Tanks.Domain.DomainModels;

namespace Tanks.Infrastructure.Repositories;

public class MongoDbMapRepository : IMapRepository
{

    public const string CollectionName = "maps";

    private readonly IMongoCollection<Map> _mapCollection;

    public MongoDbMapRepository(IMongoCollection<Map> mapCollection)
    {
        _mapCollection = mapCollection;
    }

    public Task AddMapAsync(Map map)
    {
        return _mapCollection.InsertOneAsync(map);
    }

    public async Task<Map> GetMapByIdAsync(Guid id)
    {
        var filter = Builders<Map>.Filter.Eq(entity => entity.Id, id);
        return await (await _mapCollection.FindAsync(filter)).FirstAsync();
    }
}