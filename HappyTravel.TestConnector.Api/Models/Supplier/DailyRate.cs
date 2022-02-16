using HappyTravel.EdoContracts.General.Enums;

namespace HappyTravel.TestConnector.Api.Models.Supplier;

public class DailyRate
{
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
    public string? Description { get; set; }
    public MoneyAmount Gross { get; set; }
    public MoneyAmount FinalPrice { get; set; }
    public PriceTypes Type { get; set; }
}