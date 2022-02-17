using HappyTravel.EdoContracts.Accommodations.Enums;
using HappyTravel.EdoContracts.Accommodations.Internals;

namespace HappyTravel.TestConnector.Data.Models;

public class Booking
{
    public string ReferenceCode { get; set; } = string.Empty;
    public DateTimeOffset CheckInDate { get; set; }
    public DateTimeOffset CheckOutDate { get; set; }
    public string AccommodationId { get; set; } = string.Empty;
    public List<SlimRoomOccupation> Rooms { get; set; } = new(0);
    public BookingStatusCodes Status { get; set; }
}