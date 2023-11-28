using System.ComponentModel.DataAnnotations;

using HanksMineralEmporium.Data.DatabaseIO;

namespace HanksMineralEmporium.Core.UserManagement;

/// <summary>
/// Contract for a user.
/// </summary>
public interface IUser : IDatabaseObject
{
    [StringLength(32, MinimumLength = 3)]
    public string Username { get; }
    [StringLength(72, MinimumLength = 8)]
    public string Password { get; }
    public bool IsAdmin { get; set; }
}