using HappyTravel.EdoContracts.Accommodations.Enums;
using HappyTravel.EdoContracts.General.Enums;
using HappyTravel.Money.Enums;

namespace HappyTravel.TestConnector.Api.Models;

public class GenerationOptions
{
    public int AvailabilitiesCount { get; set; }
    public decimal StartAmount { get; set; }
    public Currencies Currency { get; set; }
    public decimal AmountStep { get; set; }
    public bool IsAdvancePurchaseRate { get; set; }
    public List<CancellationOptions>? CancellationOptions { get; set; }
    public BoardBasisTypes BoardBasis { get; set; }
    public BookingStatusCodes BookingStatus { get; set; }
    public bool IsVccRequired { get; set; }
    public List<CardVendor> SupportedCardVendors { get; set; } = new();
}