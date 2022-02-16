using HappyTravel.EdoContracts.Accommodations;

namespace HappyTravel.TestConnector.Api.Infrastructure.Options;

public class AccommodationStorage
{
    public List<MultilingualAccommodation> Accommodations { get; set; } = new();
}