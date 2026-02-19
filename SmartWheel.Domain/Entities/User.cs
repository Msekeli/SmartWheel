using SmartWheel.Domain.Exceptions;
using SmartWheel.Domain.Rules;

namespace SmartWheel.Domain.Entities;

public sealed class User
{
    public Guid Id { get; private set; }

    public string Email { get; private set; } = null!;

    public int Balance { get; private set; }

    public DateTime? LastSpinUtc { get; private set; }

    // Required for ORM / deserialization
    private User() { }

    public User(Guid id, string email)
    {
        Id = id;
        Email = ValidateEmail(email);
        Balance = 0;
        LastSpinUtc = null;
    }

    public bool CanSpin(DateTime nowUtc)
    {
        return CooldownPolicy.CanSpin(LastSpinUtc, nowUtc);
    }

    public DateTime? GetNextEligibleSpinUtc()
    {
        if (!LastSpinUtc.HasValue)
            return null;

        return CooldownPolicy.GetNextEligibleSpin(LastSpinUtc.Value);
    }

    public void ApplySpin(int prizeAmount, DateTime nowUtc)
    {
        if (!CanSpin(nowUtc))
            throw new DomainException("Spin not allowed yet.");

        if (prizeAmount < 0)
            throw new DomainException("Prize amount cannot be negative.");

        Balance += prizeAmount;
        LastSpinUtc = nowUtc;
    }

    private static string ValidateEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email cannot be empty.");

        return email.Trim().ToLowerInvariant();
    }
}
