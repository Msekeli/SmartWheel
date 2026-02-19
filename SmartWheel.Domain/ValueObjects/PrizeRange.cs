namespace SmartWheel.Domain.ValueObjects;

public sealed class PrizeRange
{
    public int Min { get; }
    public int Max { get; }

    public PrizeRange(int min, int max)
    {
        if (min > max)
            throw new ArgumentException("Min cannot be greater than Max.");

        Min = min;
        Max = max;
    }

    public int GetRandomAmount()
    {
        return Random.Shared.Next(Min, Max + 1);
    }
}
