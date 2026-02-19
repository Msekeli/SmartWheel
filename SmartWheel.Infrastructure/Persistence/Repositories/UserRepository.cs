using Azure;
using Azure.Data.Tables;
using Microsoft.Extensions.Options;
using SmartWheel.Application.Interfaces;
using SmartWheel.Domain.Entities;
using SmartWheel.Infrastructure.Persistence;
using SmartWheel.Infrastructure.Persistence.TableEntities;

namespace SmartWheel.Infrastructure.Persistence.Repositories;

public sealed class UserRepository : IUserRepository
{
    private readonly TableClient _tableClient;

    public UserRepository(
        TableServiceClient tableServiceClient,
        IOptions<SmartWheelSettings> settings)
    {
        var tableName = settings.Value.UsersTable;

        _tableClient = tableServiceClient.GetTableClient(tableName);
        _tableClient.CreateIfNotExists();
    }

    public async Task<User?> GetByIdAsync(Guid userId)
    {
        try
        {
            var response = await _tableClient.GetEntityAsync<UserTableEntity>(
                partitionKey: "USER",
                rowKey: userId.ToString());

            var entity = response.Value;

            return MapToDomain(entity);
        }
        catch (RequestFailedException ex) when (ex.Status == 404)
        {
            return null;
        }
    }

    public async Task UpdateAsync(User user)
    {
        var entity = MapToTableEntity(user);

        await _tableClient.UpsertEntityAsync(entity);
    }

    private static User MapToDomain(UserTableEntity entity)
    {
        var user = new User(
            Guid.Parse(entity.RowKey),
            entity.Email);

        // Restore state
        typeof(User)
            .GetProperty(nameof(User.Balance))!
            .SetValue(user, entity.Balance);

        typeof(User)
            .GetProperty(nameof(User.LastSpinUtc))!
            .SetValue(user, entity.LastSpinUtc);

        return user;
    }

    private static UserTableEntity MapToTableEntity(User user)
    {
        return new UserTableEntity
        {
            RowKey = user.Id.ToString(),
            Email = user.Email,
            Balance = user.Balance,
            LastSpinUtc = user.LastSpinUtc
        };
    }
}
