using HappyTravel.EdoContracts.Accommodations.Internals;

namespace HappyTravel.TestConnector.Api.Services.Supplier;

public interface ISupplierService
{
    public Task<List<SlimAccommodationAvailability>> GetWideAvailabilityResult(string availabilityId, List<string> accommodationIds, 
        DateTime checkInDate, List<RoomOccupationRequest> occupancies);
}