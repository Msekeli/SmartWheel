namespace SmartWheel.Application.Interfaces;

public interface IUserRepository
{
    Task<string> GetHelloAsync();
}
