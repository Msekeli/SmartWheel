namespace SmartWheel.Application.DTOs.Spin;

public sealed class SpinResultResponse
{
    public int PrizeAmount { get; set; }
    public List<int> WheelValues { get; set; } = new();
    public int StopIndex { get; set; }
    public int NewBalance { get; set; }
    public DateTime? NextEligibleSpinUtc { get; set; }
}
