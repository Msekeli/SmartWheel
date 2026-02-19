namespace SmartWheel.Infrastructure.Persistence;

public sealed class TableNames
{
    public string Users { get; }
    public string Riddles { get; }
    public string SpinHistory { get; }

    public TableNames(SmartWheelSettings settings)
    {
        Users = settings.UsersTable;
        Riddles = settings.RiddlesTable;
        SpinHistory = settings.SpinHistoryTable;
    }
}
