using HappyTravel.EdoContracts.General.Enums;

namespace HappyTravel.TestConnector.Api.Models.Supplier;

public class Rate
{
    public string? Description { get; set; }
    public MoneyAmount Gross { get; set; }
    public List<Discount>? Discounts { get; set; }
    public MoneyAmount FinalPrice { get; set; }
    public PriceTypes Type { get; set; }
}