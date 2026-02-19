using SmartWheel.Domain.Entities;

namespace SmartWheel.Application.Interfaces;

public interface IRiddleRepository
{
    Task<Riddle?> GetByIdAsync(Guid riddleId);
}
