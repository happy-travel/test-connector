using CSharpFunctionalExtensions;
using HappyTravel.BaseConnector.Api.Infrastructure;
using HappyTravel.BaseConnector.Api.Services.Availabilities.AccommodationAvailabilities;
using HappyTravel.EdoContracts.Accommodations;
using HappyTravel.EdoContracts.Accommodations.Enums;
using Microsoft.AspNetCore.Mvc;

namespace HappyTravel.TestConnector.Api.Services.Connector;

public class AccommodationAvailabilityService : IAccommodationAvailabilityService
{
    public AccommodationAvailabilityService(IWideResultStorage resultStorage)
    {
        _resultStorage = resultStorage;
    }
    
    
    public Task<Result<AccommodationAvailability, ProblemDetails>> Get(string availabilityId, string accommodationId, CancellationToken cancellationToken)
    {
        return GetResult()
            .Bind(Convert);


        async Task<Result<Availability, ProblemDetails>> GetResult()
        {
            var (_, isFailure, availability, error) = await _resultStorage.Get(availabilityId);
            return isFailure
                ? ProblemDetailsBuilder.CreateFailureResult<Availability>(error, SearchFailureCodes.Unknown)
                : availability;
        }


        Result<AccommodationAvailability, ProblemDetails> Convert(Availability availability)
        {
            var result = availability.Results.SingleOrDefault(a => a.AccommodationId == accommodationId);

            if (result.Equals(default))
                return ProblemDetailsBuilder.CreateFailureResult<AccommodationAvailability>($"Cached result for {accommodationId} not found", SearchFailureCodes.Unknown);

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