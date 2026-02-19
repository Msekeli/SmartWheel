namespace SmartWheel.Domain.Entities;

public sealed class Riddle
{
    public Guid Id { get; private set; }

    public string Question { get; private set; } = null!;
    public string OptionA { get; private set; } = null!;
    public string OptionB { get; private set; } = null!;
    public string OptionC { get; private set; } = null!;
    public string OptionD { get; private set; } = null!;
    public string CorrectOption { get; private set; } = null!;

    // Required by ORM / deserialization
    private Riddle() { }

    public Riddle(
        Guid id,
        string question,
        string optionA,
        string optionB,
        string optionC,
        string optionD,
        string correctOption)
    {
        Id = id;
        Question = Validate(question, nameof(question));
        OptionA = Validate(optionA, nameof(optionA));
        OptionB = Validate(optionB, nameof(optionB));
        OptionC = Validate(optionC, nameof(optionC));
        OptionD = Validate(optionD, nameof(optionD));

        SetCorrectOption(correctOption);
    }

    public bool IsCorrect(string submittedOption)
    {
        if (string.IsNullOrWhiteSpace(submittedOption))
            return false;

        return string.Equals(
            CorrectOption,
            submittedOption.Trim(),
            StringComparison.OrdinalIgnoreCase);
    }

    private void SetCorrectOption(string correctOption)
    {
        var normalized = Validate(correctOption, nameof(correctOption))
            .Trim()
            .ToUpperInvariant();

        if (normalized is not ("A" or "B" or "C" or "D"))
            throw new ArgumentException("Correct option must be A, B, C, or D.");

        CorrectOption = normalized;
    }

    private static string Validate(string value, string name)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException($"{name} cannot be empty.");

        return value;
    }
}
