using SmartWheel.Application.DTOs.Spin;
using SmartWheel.Application.Interfaces;
using SmartWheel.Domain.Entities;
using SmartWheel.Domain.Exceptions;
using SmartWheel.Domain.Rules;

public sealed class ProcessSpinUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly IRiddleRepository _riddleRepository;
    private readonly ISpinHistoryRepository _spinHistoryRepository;
    private readonly SpinCalculator _spinCalculator;

    public ProcessSpinUseCase(
        IUserRepository userRepository,
        IRiddleRepository riddleRepository,
        ISpinHistoryRepository spinHistoryRepository,
        SpinCalculator spinCalculator)
    {
        _userRepository = userRepository;
        _riddleRepository = riddleRepository;
        _spinHistoryRepository = spinHistoryRepository;
        _spinCalculator = spinCalculator;
    }

    public async Task<SpinResultResponse> ExecuteAsync(
        Guid userId,
        ProcessSpinRequest request)
    {
        var user = await _userRepository.GetByIdAsync(userId)
            ?? throw new DomainException("User not found.");

        var nowUtc = DateTime.UtcNow;

        if (!user.CanSpin(nowUtc))
        {
            var next = user.GetNextEligibleSpinUtc();
            throw new DomainException(
                $"Next spin available at {next:O}");
        }

        int correctAnswers = 0;

        foreach (var submission in request.Answers)
        {
            var riddle = await _riddleRepository.GetByIdAsync(submission.RiddleId)
                ?? throw new DomainException("Invalid riddle.");

            if (riddle.IsCorrect(submission.Answer))
                correctAnswers++;
        }

        var spinResult = _spinCalculator.Calculate(correctAnswers);

        user.ApplySpin(spinResult.PrizeAmount, nowUtc);

        var history = SpinHistory.Create(
            user.Id,
            spinResult.PrizeAmount,
            nowUtc);

        await _spinHistoryRepository.AddAsync(history);
        await _userRepository.UpdateAsync(user);

        return new SpinResultResponse
        {
            PrizeAmount = spinResult.PrizeAmount,
            WheelValues = spinResult.WheelValues,
            StopIndex = spinResult.StopIndex,
            NewBalance = user.Balance,
            NextEligibleSpinUtc = user.GetNextEligibleSpinUtc()
        };
    }
}
