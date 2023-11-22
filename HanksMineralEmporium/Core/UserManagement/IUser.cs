using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

using HanksMineralEmporium.Data.DatabaseIO;

namespace HanksMineralEmporium.Core.UserManagement;

/// <summary>
/// Contract for a user.
/// </summary>
public interface IUser : IDatabaseObject
{
    [NotNull, StringLength(32, MinimumLength = 3)]
    public string Username { get; }
    [NotNull, StringLength(72, MinimumLength = 8)]
    public string Password { get; }
    [NotNull]
    public bool IsAdmin { get; set; }
}