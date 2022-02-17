using HappyTravel.EdoContracts.Accommodations.Internals;

namespace HappyTravel.TestConnector.Api.Services.Supplier;

public interface ISupplierService
{
    public List<SlimAccommodationAvailability> GetWideAvailabilityResult(string availabilityId, List<string> accommodationIds, 
        DateTime checkInDate, List<RoomOccupationRequest> occupancies);
}