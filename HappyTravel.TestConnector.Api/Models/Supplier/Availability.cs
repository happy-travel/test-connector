using MongoDB.Bson;
using Newtonsoft.Json;

namespace HappyTravel.TestConnector.Api.Models.Supplier;

public class Availability
{
    [JsonIgnore]
    public ObjectId Id { get; set; }
    public string ScenarioName { get; set; } = string.Empty;
    public string AvailabilityId { get; set; } = string.Empty;
    public DateTime CheckInDate { get; set; }
    public DateTime CheckOutDate { get; set; }
    public int NumberOfNights { get; set; }
    public int NumberOfProcessedAccommodations { get; set; }
    public List<SlimAccommodationAvailability> Results { get; set; } = new();
}