using CSharpFunctionalExtensions;
using HappyTravel.BaseConnector.Api.Services.Availabilities.RoomContractSetAvailabilities;
using HappyTravel.EdoContracts.Accommodations;

namespace HappyTravel.TestConnector.Api.Services.Connector;

public class RoomContractSetAvailabilityService : IRoomContractSetAvailabilityService
{
    public async Task<Result<RoomContractSetAvailability?>> Get(string availabilityId, Guid roomContractSetId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}