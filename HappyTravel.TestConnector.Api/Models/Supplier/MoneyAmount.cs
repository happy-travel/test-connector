using HappyTravel.Money.Enums;

namespace HappyTravel.TestConnector.Api.Models.Supplier;

public class MoneyAmount
{
    public decimal Amount { get; set; }
    public Currencies Currency { get; set; }
}