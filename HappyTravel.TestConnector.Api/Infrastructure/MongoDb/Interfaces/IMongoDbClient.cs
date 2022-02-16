using MongoDB.Driver;

namespace HappyTravel.TestConnector.Api.Infrastructure.MongoDb.Interfaces;

public interface IMongoDbClient
{
    public IMongoDatabase GetDatabase();
}