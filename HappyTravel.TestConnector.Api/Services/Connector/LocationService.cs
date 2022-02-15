using HappyTravel.BaseConnector.Api.Services.Locations;
using HappyTravel.EdoContracts.GeoData;
using HappyTravel.EdoContracts.GeoData.Enums;

namespace HappyTravel.TestConnector.Api.Services.Connector;

public class LocationService : ILocationService
{
    public async Task<List<Location>> Get(DateTime modified, LocationTypes locationType, int skip, int top, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}