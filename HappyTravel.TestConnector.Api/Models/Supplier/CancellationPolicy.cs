namespace HappyTravel.TestConnector.Api.Models.Supplier;

public class CancellationPolicy
{
    public DateTime FromDate { get; set; }
    public double Percentage { get; set; }
    public string? Remark { get; set; }
}