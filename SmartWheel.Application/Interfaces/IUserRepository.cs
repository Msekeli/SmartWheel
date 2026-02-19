using SmartWheel.Domain.Entities;

namespace SmartWheel.Application.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid userId);
    Task UpdateAsync(User user);
}
