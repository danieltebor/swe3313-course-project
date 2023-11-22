namespace HanksMineralEmporium.Core.UserManagement;

public class User : IUser
{
    public User(ulong id, string username, string password, bool isAdmin = false)
    {
        Id = id;
        Username = username;
        Password = password;
        IsAdmin = isAdmin;
    }

    public ulong Id { get; }
    public string Username { get; }
    public string Password { get; }
    public bool IsAdmin { get; }
}