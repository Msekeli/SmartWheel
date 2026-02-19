using SmartWheel.Application.Interfaces;

namespace SmartWheel.Infrastructure.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    public Task<string> GetHelloAsync()
    {
        return Task.FromResult("Infrastructure is connected.");
    }
}
