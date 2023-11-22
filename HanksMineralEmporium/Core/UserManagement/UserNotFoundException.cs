namespace HanksMineralEmporium.Core.UserManagement;

class UserNotFoundException : Exception
{
    public UserNotFoundException(ulong id) : base($"User with ID {id} not found.") {}
}