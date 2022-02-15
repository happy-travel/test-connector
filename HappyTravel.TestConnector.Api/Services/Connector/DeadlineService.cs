using CSharpFunctionalExtensions;
using HappyTravel.BaseConnector.Api.Services.Availabilities.Cancellations;
using HappyTravel.EdoContracts.Accommodations;

namespace HappyTravel.TestConnector.Api.Services.Connector;

public class DeadlineService : IDeadlineService
{
    public async Task<Result<Deadline>> Get(string availabilityId, Guid roomContractSetId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}