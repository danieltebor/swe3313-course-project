using HanksMineralEmporium.Core.InventoryManagement;

namespace HanksMineralEmporium.Data.DatabaseIO;

/// <summary>
/// Contract for a database operator that handles <see cref="IItem"/> objects.
/// </summary>
internal interface IItemDatabaseOperator : IDatabaseOperator<IItem> {}