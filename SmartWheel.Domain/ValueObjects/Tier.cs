namespace SmartWheel.Domain.ValueObjects;

public sealed class Tier
{
    public int Level { get; }
    public PrizeRange PrizeRange { get; }

    private Tier(int level, PrizeRange prizeRange)
    {
        Level = level;
        PrizeRange = prizeRange;
    }

    public static Tier FromScore(int correctAnswers)
    {
        return correctAnswers switch
        {
            0 => new Tier(0, new PrizeRange(0, 0)),
            1 => new Tier(1, new PrizeRange(1, 25)),
            2 => new Tier(2, new PrizeRange(26, 50)),
            3 => new Tier(3, new PrizeRange(51, 75)),
            4 => new Tier(4, new PrizeRange(76, 100)),
            5 => new Tier(5, new PrizeRange(101, 125)),
            _ => throw new ArgumentOutOfRangeException(nameof(correctAnswers))
        };
    }
}
