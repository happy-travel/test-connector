using CSharpFunctionalExtensions;
using HappyTravel.BaseConnector.Api.Services.Availabilities.RoomContractSetAvailabilities;
using HappyTravel.EdoContracts.Accommodations;

namespace HappyTravel.TestConnector.Api.Services.Connector;

public class RoomContractSetAvailabilityService : IRoomContractSetAvailabilityService
{
    public RoomContractSetAvailabilityService(IWideResultStorage resultStorage)
    {
        _resultStorage = resultStorage;
    }

    
    public async Task<Result<RoomContractSetAvailability?>> Get(string availabilityId, Guid roomContractSetId, CancellationToken cancellationToken)
    {
        return await _resultStorage.Get(availabilityId)
            .Bind(Convert);


        Result<RoomContractSetAvailability?> Convert(Availability availability)
        {
            var result = availability.Results
                .SingleOrDefault(a => a.RoomContractSets.Any(r => r.Id == roomContractSetId));

            if (result.Equals(default))
                return Result.Failure<RoomContractSetAvailability?>($"Cached result for {roomContractSetId} not found");

            var roomContractSet = result.RoomContractSets.SingleOrDefault(r => r.Id == roomContractSetId);

            return new RoomContractSetAvailability(availabilityId: availabilityId,
                accommodationId: result.AccommodationId,
                checkInDate: availability.CheckInDate,
                checkOutDate: availability.CheckOutDate,
                numberOfNights: availability.NumberOfNights,
                roomContractSet: roomContractSet);
        }
    }


    private readonly IWideResultStorage _resultStorage;
}