using CSharpFunctionalExtensions;
using FloxDc.CacheFlow;
using FloxDc.CacheFlow.Extensions;
using HappyTravel.EdoContracts.Accommodations;

namespace HappyTravel.TestConnector.Api.Services.Connector;

public class WideResultStorage : IWideResultStorage
{
    public WideResultStorage(IDoubleFlow cache)
    {
        _cache = cache;
    }


    public async Task<Result<Availability>> Get(string availabilityId)
    {
        var key = BuildKey(availabilityId);
        var result = await _cache.GetAsync<Availability?>(key, Expiration);
        return result ?? Result.Failure<Availability>($"Cached results for {availabilityId} not found");
    }

    public async Task Set(string availabilityId, Availability availability)
    {
        var key = BuildKey(availabilityId);
        await _cache.SetAsync(key, availability, Expiration);
    }


    private string BuildKey(string availabilityId) 
        => _cache.BuildKey(nameof(WideResultStorage), availabilityId);


    public static TimeSpan Expiration = TimeSpan.FromMinutes(45);
    private readonly IDoubleFlow _cache;
}