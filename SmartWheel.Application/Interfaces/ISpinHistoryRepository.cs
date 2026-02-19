using SmartWheel.Domain.Entities;

namespace SmartWheel.Application.Interfaces;

public interface ISpinHistoryRepository
{
    Task AddAsync(SpinHistory spinHistory);
}
