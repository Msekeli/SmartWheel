namespace SmartWheel.Domain.Entities;

public sealed class SpinHistory
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public int PrizeAmount { get; private set; }
    public DateTime CreatedUtc { get; private set; }

    private SpinHistory() { } // For persistence

    private SpinHistory(Guid id, Guid userId, int prizeAmount, DateTime createdUtc)
    {
        Id = id;
        UserId = userId;
        PrizeAmount = prizeAmount;
        CreatedUtc = createdUtc;
    }

    public static SpinHistory Create(Guid userId, int prizeAmount, DateTime createdUtc)
    {
        return new SpinHistory(
            Guid.NewGuid(),
            userId,
            prizeAmount,
            createdUtc
        );
    }
}
