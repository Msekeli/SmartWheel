namespace SmartWheel.Domain.Entities;

public sealed class SpinHistory
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public int PrizeAmount { get; private set; }
    public DateTime CreatedUtc { get; private set; }

    // Required for ORM / deserialization
    private SpinHistory() { }

    private SpinHistory(Guid id, Guid userId, int prizeAmount, DateTime createdUtc)
    {
        if (userId == Guid.Empty)
            throw new ArgumentException("UserId cannot be empty.");

        if (prizeAmount < 0)
            throw new ArgumentException("Prize amount cannot be negative.");

        if (createdUtc == default)
            throw new ArgumentException("CreatedUtc must be a valid date.");

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
