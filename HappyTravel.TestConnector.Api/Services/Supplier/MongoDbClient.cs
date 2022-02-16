using HappyTravel.TestConnector.Api.Infrastructure.MongoDb.Interfaces;
using HappyTravel.TestConnector.Api.Infrastructure.MongoDb.Options;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace HappyTravel.TestConnector.Api.Services.Supplier;

public class MongoDbClient : IMongoDbClient
{
    public MongoDbClient(IOptions<MongoDbOptions> options)
    {
        _client = new MongoClient(options.Value.ConnectionString);
        _databaseName = options.Value.DatabaseName;
    }


    public IMongoDatabase GetDatabase()
        => _client.GetDatabase(_databaseName);

    private readonly string? _databaseName;
    private readonly MongoClient _client;
}