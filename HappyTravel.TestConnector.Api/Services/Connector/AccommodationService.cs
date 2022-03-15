using HappyTravel.BaseConnector.Api.Services.Accommodations;
using HappyTravel.EdoContracts.Accommodations;
using HappyTravel.TestConnector.Api.Infrastructure.Options;
using Microsoft.Extensions.Options;

namespace HappyTravel.TestConnector.Api.Services.Connector;

public class AccommodationService : IAccommodationService
{
    public AccommodationService(IOptions<AccommodationStorage> options)
    {
        _accommodationStorage = options.Value;
    }


    public Task<List<MultilingualAccommodation>> Get(int skip, int top, DateTimeOffset? modificationDate, CancellationToken cancellationToken) 
        => Task.FromResult(_accommodationStorage.Accommodations.Take(top).Skip(skip).ToList());


    private readonly AccommodationStorage _accommodationStorage;
}