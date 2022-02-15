using CSharpFunctionalExtensions;
using HappyTravel.BaseConnector.Api.Services.Availabilities.AccommodationAvailabilities;
using HappyTravel.EdoContracts.Accommodations;

namespace HappyTravel.TestConnector.Api.Services.Connector;

public class AccommodationAvailabilityService : IAccommodationAvailabilityService
{
    public async Task<Result<AccommodationAvailability>> Get(string availabilityId, string accommodationId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}