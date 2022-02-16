using CSharpFunctionalExtensions;
using HappyTravel.EdoContracts.Accommodations;

namespace HappyTravel.TestConnector.Api.Services.Connector;

public interface IWideResultStorage
{
    public Task<Result<Availability>> Get(string availabilityId);
    public Task Set(string availabilityId, Availability availability);
}