using HanksMineralEmporium.Core.SalesManagement;

namespace HanksMineralEmporium.Data.DatabaseIO.Json;

/// <summary>
/// JSON implementation of <see cref="IReceiptDatabaseOperator"/>.
/// </summary>
internal class JsonReceiptDatabaseOperator : JsonDatabaseOperator<IReceipt>, IReceiptDatabaseOperator
{
    private static readonly string DatabaseName = "Receipts";

    protected override IReadOnlyList<IReceipt> GetSeedData()
    {
        List<IReceipt> seedData = new();
        return seedData;
    }

    public JsonReceiptDatabaseOperator()
        : base(DatabaseName, new JsonDatabaseObjectSerializer<IReceipt>()) {}

    /// <inheritdoc/>
    public async Task<IReadOnlyList<IReceipt>> GetReceiptsByUserIdAsync(ulong userId)
    {
        await _databaseLock.WaitAsync();
        try
        {
            var jsonFileStr = File.ReadAllText(_databasePath);
            if (string.IsNullOrWhiteSpace(jsonFileStr))
            {
                return new List<IReceipt>();
            }
            var receipts = _jsonSerializer.DeserializeList(jsonFileStr);

            return receipts.Where(r => r.UserId == userId).ToList();
        }
        finally
        {
            _databaseLock.Release();
        }
    }
}