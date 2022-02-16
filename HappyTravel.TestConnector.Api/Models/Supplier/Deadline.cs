namespace HappyTravel.TestConnector.Api.Models.Supplier;

public class Deadline
{
    public DateTime? Date { get; set; }
    public bool IsFinal { get; set; }
    public List<CancellationPolicy> Policies { get; set; } = new();
    public List<string> Remarks { get; set; } = new();
}