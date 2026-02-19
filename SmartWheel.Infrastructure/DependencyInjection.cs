using Microsoft.Extensions.DependencyInjection;

namespace SmartWheel.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        // We will register repositories here later

        return services;
    }
}
