namespace SmartWheel.Domain.Entities;

public sealed class Riddle
{
    public Guid Id { get; private set; }
    public string Question { get; private set; }
    public string OptionA { get; private set; }
    public string OptionB { get; private set; }
    public string OptionC { get; private set; }
    public string OptionD { get; private set; }
    public string CorrectOption { get; private set; }

    private Riddle() { } // For persistence

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
        Question = question;
        OptionA = optionA;
        OptionB = optionB;
        OptionC = optionC;
        OptionD = optionD;

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
        var normalized = correctOption.Trim().ToUpperInvariant();

        if (normalized is not ("A" or "B" or "C" or "D"))
            throw new ArgumentException("Correct option must be A, B, C, or D.");

        CorrectOption = normalized;
    }
}
