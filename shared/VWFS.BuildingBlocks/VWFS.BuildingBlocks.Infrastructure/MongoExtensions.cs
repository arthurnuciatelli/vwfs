using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace VWFS.BuildingBlocks.Infrastructure;

public static class MongoExtensions
{
    public static IServiceCollection AddMongo(this IServiceCollection services, string connection, string databaseName)
    {
        services.AddSingleton<IMongoClient>(_ => new MongoClient(connection));
        services.AddScoped(s => s.GetRequiredService<IMongoClient>().GetDatabase(databaseName));
        return services;
    }
}
