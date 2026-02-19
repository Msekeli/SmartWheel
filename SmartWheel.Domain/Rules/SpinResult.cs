namespace SmartWheel.Domain.Rules;

public sealed record SpinResult(
    int PrizeAmount,
    List<int> WheelValues,
    int StopIndex
);
