using HappyTravel.TestConnector.Api.Infrastructure.MongoDb.Interfaces;
using HappyTravel.TestConnector.Api.Models.Supplier;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace HappyTravel.TestConnector.Api.Services.Supplier;

public class AvailabilityStorage : IMongoDbStorage<Availability>
{
    public AvailabilityStorage(IMongoDbClient client)
    {
        _client = client;
    }


    public IMongoQueryable<Availability> Collection() 
        => GetCollection().AsQueryable();


    public Task Add(IEnumerable<Availability> records) 
        => GetCollection().InsertManyAsync(records);


    public Task Add(Availability record) 
        => GetCollection().InsertOneAsync(record);


    private IMongoCollection<Availability> GetCollection()
        => _client.GetDatabase().GetCollection<Availability>(nameof(Availability));


    private readonly IMongoDbClient _client;
}