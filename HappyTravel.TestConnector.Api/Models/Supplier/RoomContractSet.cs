namespace HappyTravel.TestConnector.Api.Models.Supplier;

public class RoomContractSet
{
    public Rate Rate { get; set; }
    public Deadline Deadline { get; set; }
    public bool IsAdvancePurchaseRate { get; set; }
    public bool IsPackageRate { get; set; }
    public List<RoomContract> RoomContracts { get; set; } = new();
    public List<string> Tags { get; set; } = new();
    public bool IsDirectContract { get; set; }
}