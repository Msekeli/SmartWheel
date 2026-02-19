using Azure;
using Azure.Data.Tables;

namespace SmartWheel.Infrastructure.Persistence.TableEntities;

public sealed class UserTableEntity : ITableEntity
{
    public string PartitionKey { get; set; } = "USER";
    public string RowKey { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;
    public int Balance { get; set; }
    public DateTime? LastSpinUtc { get; set; }

    public ETag ETag { get; set; }
    public DateTimeOffset? Timestamp { get; set; }
}
