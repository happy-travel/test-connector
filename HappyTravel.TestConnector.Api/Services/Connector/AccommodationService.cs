using HappyTravel.BaseConnector.Api.Services.Accommodations;
using HappyTravel.EdoContracts.Accommodations;

namespace HappyTravel.TestConnector.Api.Services.Connector;

public class AccommodationService : IAccommodationService
{
    public async Task<List<MultilingualAccommodation>> Get(int skip, int top, DateTime? modificationDate, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}