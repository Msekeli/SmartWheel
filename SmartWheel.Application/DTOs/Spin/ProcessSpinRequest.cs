namespace SmartWheel.Application.DTOs.Spin;

public sealed class ProcessSpinRequest
{
    public Guid UserId { get; set; }
    public List<RiddleAnswerDto> Answers { get; set; } = new();
}

public sealed class RiddleAnswerDto
{
    public Guid RiddleId { get; set; }
    public string Answer { get; set; } = string.Empty;
}
