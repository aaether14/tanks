namespace Tanks.Infrastructure.Configuration;

public class MongoDbSettings
{
    public const string SectionName = "MongoDbSettings";

    public string ConnectionString { get; init; } = null!;
    public string DatabaseName { get; init; } = null!;
    public MongoDbCollectionNames CollectionNames { get; init; } = null!;

}