using CSharpFunctionalExtensions;
using HappyTravel.BaseConnector.Api.Services.Availabilities.RoomContractSetAvailabilities;
using HappyTravel.EdoContracts.Accommodations;
using HappyTravel.EdoContracts.Accommodations.Internals;
using HappyTravel.EdoContracts.General.Enums;
using HappyTravel.TestConnector.Api.Models;
using Microsoft.Extensions.Options;

namespace HappyTravel.TestConnector.Api.Services.Connector;

public class RoomContractSetAvailabilityService : IRoomContractSetAvailabilityService
{
    public RoomContractSetAvailabilityService(IWideResultStorage resultStorage, IOptionsMonitor<Dictionary<string, GenerationOptions>> optionsStorage)
    {
        _resultStorage = resultStorage;
        _optionsStorage = optionsStorage;
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
            
            if (!_optionsStorage.CurrentValue.TryGetValue(result.AccommodationId, out var options))
                return Result.Failure<RoomContractSetAvailability?>("Options not found");

            CreditCardRequirement? creditCardRequirement = options.IsVccRequired
                ? new CreditCardRequirement(activationDate: availability.CheckInDate, 
                    dueDate: availability.CheckOutDate, 
                    supportedCardVendors: options.SupportedCardVendors)
                : null;
            
            
            return new RoomContractSetAvailability(availabilityId: availabilityId,
                accommodationId: result.AccommodationId,
                checkInDate: availability.CheckInDate,
                checkOutDate: availability.CheckOutDate,
                numberOfNights: availability.NumberOfNights,
                roomContractSet: roomContractSet,
                creditCardRequirement: creditCardRequirement);
        }
    }


    private readonly IWideResultStorage _resultStorage;
    private readonly IOptionsMonitor<Dictionary<string, GenerationOptions>> _optionsStorage;
}