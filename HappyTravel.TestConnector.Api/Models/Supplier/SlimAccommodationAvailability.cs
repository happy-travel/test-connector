namespace HappyTravel.TestConnector.Api.Models.Supplier;

public class SlimAccommodationAvailability
{
    public string AccommodationId { get; set; } = string.Empty;
    public string AvailabilityId { get; set; } = string.Empty;
    public List<RoomContractSet> RoomContractSets { get; set; } = new();
}