namespace SmartWheel.Infrastructure.Persistence;

public sealed class SmartWheelSettings
{
    public string UsersTable { get; set; } = string.Empty;
    public string RiddlesTable { get; set; } = string.Empty;
    public string SpinHistoryTable { get; set; } = string.Empty;
}
