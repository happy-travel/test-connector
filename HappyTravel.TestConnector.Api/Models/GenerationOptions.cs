using HappyTravel.Money.Enums;

namespace HappyTravel.TestConnector.Api.Models;

public class GenerationOptions
{
    public int AvailabilitiesCount { get; set; }
    public decimal StartAmount { get; set; }
    public Currencies Currency { get; set; }
    public decimal AmountStep { get; set; }
    public bool IsAdvancePurchaseRate { get; set; }
    public TimeSpan DeadlineOffset { get; set; }
    public double CancellationPercentage { get; set; } 
}