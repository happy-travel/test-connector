using CSharpFunctionalExtensions;
using HappyTravel.BaseConnector.Api.Services.Availabilities.WideAvailabilities;
using HappyTravel.EdoContracts.Accommodations;

namespace HappyTravel.TestConnector.Api.Services.Connector;

public class WideAvailabilitySearchService : IWideAvailabilitySearchService
{
    public async Task<Result<Availability>> Get(AvailabilityRequest request, string languageCode, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}