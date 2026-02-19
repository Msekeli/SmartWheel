namespace SmartWheel.Domain.Rules;

public static class CooldownPolicy
{
    private static readonly TimeSpan Cooldown = TimeSpan.FromHours(24);

    public static bool CanSpin(DateTime? lastSpinUtc, DateTime nowUtc)
    {
        if (!lastSpinUtc.HasValue)
            return true;

        return nowUtc - lastSpinUtc.Value >= Cooldown;
    }

    public static DateTime GetNextEligibleSpin(DateTime lastSpinUtc)
    {
        return lastSpinUtc.Add(Cooldown);
    }
}
