using HanksMineralEmporium.Core.SalesManagement;

namespace HanksMineralEmporium.Data.DatabaseIO.Json;

/// <summary>
/// JSON implementation of <see cref="ISalesDatabaseOperator"/>.
/// </summary>
internal class JsonSalesDatabaseOperator : JsonDatabaseOperator<ISale>, ISalesDatabaseOperator
{
    private static readonly string DatabaseName = "Sales";

    protected override IReadOnlyList<ISale> GetSeedData()
    {
        return new List<ISale>();
    }

    public JsonSalesDatabaseOperator()
        : base(DatabaseName, new JsonDatabaseObjectSerializer<ISale>()) {}

    private List<ISale> GetAllSalesHelper()
    {
        var jsonFileStr = File.ReadAllText(_databasePath);
        if (string.IsNullOrWhiteSpace(jsonFileStr))
        {
            return new List<ISale>();
        }
        var sales = _jsonSerializer.DeserializeList(jsonFileStr);
        return sales ?? new List<ISale>();
    }

    /// <inheritdoc/>
    public async Task<IReadOnlyList<ISale>> GetSalesByReceiptIdAsync(ulong receiptId)
    {
        await _databaseLock.WaitAsync();
        try
        {
            var sales = GetAllSalesHelper();

            return sales.Where(s => s.ReceiptId == receiptId).ToList();
        }
        finally
        {
            _databaseLock.Release();
        }
    }

    /// <inheritdoc/>
    public async Task<IReadOnlyList<ulong>> GetSoldItemIdsAsync()
    {
        await _databaseLock.WaitAsync();
        try
        {
            var sales = GetAllSalesHelper();

            return sales.Select(s => s.ItemId).ToList();
        }
        finally
        {
            _databaseLock.Release();
        }
    }
}