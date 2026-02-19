namespace SmartWheel.Domain.Rules;

using SmartWheel.Domain.ValueObjects;

public sealed class SpinCalculator
{
    public SpinResult Calculate(int correctAnswers)
    {
        var tier = Tier.FromScore(correctAnswers);

        var winningAmount = tier.PrizeRange.GetRandomAmount();

        var wheelValues = GenerateWheel(winningAmount, out int stopIndex);

        return new SpinResult(
            winningAmount,
            wheelValues,
            stopIndex
        );
    }

    private List<int> GenerateWheel(int winningAmount, out int stopIndex)
    {
        var wheel = new List<int>();

        // 9 random noise values (0–125)
        for (int i = 0; i < 9; i++)
        {
            wheel.Add(Random.Shared.Next(0, 126));
        }

        // Insert winning value at random index
        stopIndex = Random.Shared.Next(0, 10);
        wheel.Insert(stopIndex, winningAmount);

        return wheel;
    }
}
