using Microsoft.Extensions.DependencyInjection;
using SmartWheel.Application.Interfaces;
using SmartWheel.Infrastructure.Persistence.Repositories;


namespace SmartWheel.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        //Register repositories here :
       services.AddScoped<IUserRepository, UserRepository>(); 

        return services;
    }
}
