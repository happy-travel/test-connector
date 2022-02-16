using HappyTravel.EdoContracts.Accommodations.Enums;

namespace HappyTravel.TestConnector.Api.Models.Supplier;

public class RoomContract
{
    public BoardBasisTypes BoardBasis { get; set; }
    public string? MealPlan { get; set; }
    public int ContractTypeCode { get; set; }
    public bool IsAvailableImmediately { get; set; }
    public bool IsDynamic { get; set; }
    public string? ContractDescription { get; set; }
    public Rate Rate { get; set; }
    public List<KeyValuePair<string, string>> Remarks { get; set; } = new();
    public int AdultsNumber { get; set; }
    public List<int>? ChildrenAges { get; set; }
    public bool IsExtraBedNeeded { get; set; }
    public Deadline Deadline { get; set; }
    public bool IsAdvancePurchaseRate { get; set; }
    public List<DailyRate> DailyRoomRates { get; set; } = new();
    public RoomTypes Type { get; set; }
}