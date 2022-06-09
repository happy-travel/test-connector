using HappyTravel.EdoContracts.Accommodations.Internals;

namespace HappyTravel.TestConnector.Api.Services.Supplier;

public interface ISupplierService
{
    public IEnumerable<SlimAccommodationAvailability> GetWideAvailabilityResult(string availabilityId, List<string> accommodationIds, 
        DateTime checkInDate, List<RoomOccupationRequest> occupancies);
}