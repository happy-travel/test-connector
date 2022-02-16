using CSharpFunctionalExtensions;
using HappyTravel.BaseConnector.Api.Services.Availabilities.AccommodationAvailabilities;
using HappyTravel.EdoContracts.Accommodations;
using HappyTravel.EdoContracts.Accommodations.Internals;

namespace HappyTravel.TestConnector.Api.Services.Connector;

public class AccommodationAvailabilityService : IAccommodationAvailabilityService
{
    public AccommodationAvailabilityService(IWideResultStorage resultStorage)
    {
        _resultStorage = resultStorage;
    }
    
    
    public async Task<Result<AccommodationAvailability>> Get(string availabilityId, string accommodationId, CancellationToken cancellationToken)
    {
        return await _resultStorage.Get(availabilityId)
            .Bind(Convert);


        Result<AccommodationAvailability> Convert(Availability availability)
        {
            var result = availability.Results.SingleOrDefault(a => a.AccommodationId == accommodationId);

            if (result.Equals(default))
                return Result.Failure<AccommodationAvailability>($"Cached result for {accommodationId} not found");

            return new AccommodationAvailability(availabilityId: availabilityId,
                accommodationId: accommodationId,
                checkInDate: availability.CheckInDate,
                checkOutDate: availability.CheckOutDate,
                numberOfNights: availability.NumberOfNights,
                roomContractSets: result.RoomContractSets);
        }
    }


    private readonly IWideResultStorage _resultStorage;
}