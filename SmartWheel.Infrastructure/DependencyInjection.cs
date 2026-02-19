using Azure.Data.Tables;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SmartWheel.Application.Interfaces;
using SmartWheel.Infrastructure.Persistence;
using SmartWheel.Infrastructure.Persistence.Repositories;

namespace SmartWheel.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // Bind SmartWheelSettings from local.settings.json
        services.Configure<SmartWheelSettings>(
            configuration.GetSection("SmartWheelSettings"));

        // Azure Table Service Client
        var connectionString = configuration.GetValue<string>("AzureWebJobsStorage");

        services.AddSingleton(_ => new TableServiceClient(connectionString));

        // Repositories
        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}
