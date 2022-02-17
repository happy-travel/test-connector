using HappyTravel.EdoContracts.Accommodations.Internals;
using HappyTravel.TestConnector.Api.Infrastructure.Options;
using Microsoft.Extensions.Options;

namespace HappyTravel.TestConnector.Api.Services.Supplier;

public class SupplierService : ISupplierService
{
    public SupplierService(IOptionsMonitor<Dictionary<string, Models.GenerationOptions>> optionsStorage)
    {
        _optionsStorage = optionsStorage;
    }

    
    public List<SlimAccommodationAvailability> GetWideAvailabilityResult(string availabilityId, 
        List<string> accommodationIds, DateTime checkInDate, List<RoomOccupationRequest> occupancies)
    {
        var results = new List<SlimAccommodationAvailability>();

        foreach (var accommodationId in accommodationIds)
        {
            if (!_optionsStorage.CurrentValue.TryGetValue(accommodationId, out var options))
                continue;

            results.Add(WideResultGenerator.Generate(availabilityId: availabilityId, 
                accommodationId: accommodationId, 
                checkinDate: checkInDate, 
                occupancies: occupancies,
                options: options));
        }

        return results;
    }


    private readonly IOptionsMonitor<Dictionary<string, Models.GenerationOptions>> _optionsStorage;
}