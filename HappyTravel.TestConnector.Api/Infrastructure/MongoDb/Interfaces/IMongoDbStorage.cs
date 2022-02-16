using MongoDB.Driver.Linq;

namespace HappyTravel.TestConnector.Api.Infrastructure.MongoDb.Interfaces;

public interface IMongoDbStorage<TRecord>
{
    IMongoQueryable<TRecord> Collection();
    Task Add(IEnumerable<TRecord> records);
    Task Add(TRecord record);
}