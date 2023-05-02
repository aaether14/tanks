using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using Tanks.Application.Repositories;
using Tanks.Domain.DomainModels;
using Tanks.Domain.DomainModels.TankActions;
using Tanks.Infrastructure.Configuration;
using Tanks.Infrastructure.Repositories;
using Tanks.Infrastructure.Serializers;

namespace Tanks.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        ConfigurationManager configuration)
    {
        RegisterSerializers();
        return services.AddRepositories(configuration);
    }

    private static void RegisterSerializers()
    {
        BsonClassMap.RegisterClassMap<Map>(cm =>
        {
            cm.AutoMap();
            cm.MapProperty(m => m.Grid).SetSerializer(new TwoDimensionalIntArraySerializer());
        });

        // This looks very hacky and is required, apparently, only for the latest 2.19 release.
        var objectSerializer = new ObjectSerializer(type => 
            ObjectSerializer.DefaultAllowedTypes(type) || 
            type!.FullName!.StartsWith("Tanks.Domain.DomainModels.TankActions"));
        BsonSerializer.RegisterSerializer(objectSerializer);
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services,
        ConfigurationManager configuration)
    {
        // Get MongoDB connection settings from configuration
        MongoDbSettings mongoDbSettings = configuration.GetSection(MongoDbSettings.SectionName).Get<MongoDbSettings>()!;

        // Create MongoClient instance
        IMongoClient mongoClient = new MongoClient(mongoDbSettings.ConnectionString);

        // Get database instance
        IMongoDatabase database = mongoClient.GetDatabase(mongoDbSettings.DatabaseName);

        services.AddSingleton<IRepository<Tank, Guid>>(_ =>
        {
            return new MongoDbRepository<Tank, Guid>(database.GetCollection<Tank>(mongoDbSettings.CollectionNames.Tanks));
        });
        services.AddSingleton<IRepository<Map, Guid>>(_ =>
        {
            return new MongoDbRepository<Map, Guid>(database.GetCollection<Map>(mongoDbSettings.CollectionNames.Maps));
        });
        services.AddSingleton<IRepository<Simulation, Guid>>(_ =>
        {
            return new MongoDbRepository<Simulation, 
                Guid>(database.GetCollection<Simulation>(mongoDbSettings.CollectionNames.Simulations));
        });

        return services;
    }

}