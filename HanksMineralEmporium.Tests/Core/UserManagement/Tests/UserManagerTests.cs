using HanksMineralEmporium.Core.UserManagement.Exception;
using HanksMineralEmporium.Data.DatabaseIO;

namespace HanksMineralEmporium.Core.UserManagement.Tests;

public class UserManagerTests
{
    [Fact]
    public void UserManager_WithValidDatabase_DoesNotThrow()
    {
        // Arrange.
        var mockDatabase = new Mock<IUserDatabaseOperator>();

        // Act.
        var result = Record.Exception(() => new UserManager(mockDatabase.Object));

        // Assert.
        Assert.Null(result);
    }

    [Fact]
    public void UserManager_WithNullDatabase_ThrowsArgumentNullException()
    {
        // Arrange.
        IUserDatabaseOperator? database = null;

        // Act.
        #pragma warning disable CS8604 // Possible null reference argument.
        var result = Assert.Throws<ArgumentNullException>(() => new UserManager(database));
        #pragma warning restore CS8604 // Possible null reference argument.

        // Assert.
        Assert.Equal("Value cannot be null. (Parameter 'userDatabaseOperator')", result.Message);
    }

    [Fact]
    public void RegisterUserAsync_WithValidUsernameAndPassword_ReturnsUser()
    {
        // Arrange.
        var mockDatabase = new Mock<IUserDatabaseOperator>();
        mockDatabase.Setup(x => x.IsUsernameTakenAsync(It.IsAny<string>())).ReturnsAsync(false);
        mockDatabase.Setup(x => x.GetNewUniqueId()).Returns(1);
        mockDatabase.Setup(x => x.SaveAsync(It.IsAny<IUser>()));

        IUserManager userManager = new UserManager(mockDatabase.Object);

        // Act.
        var result = userManager.RegisterUserAsync("TestUser", "TestPassword").Result;

        // Assert.
        Assert.NotNull(result);
    }

    [Fact]
    public void RegisterUserAsync_WithNullUsername_ThrowsArgumentNullException()
    {
        // Arrange.
        var mockDatabase = new Mock<IUserDatabaseOperator>();
        mockDatabase.Setup(x => x.IsUsernameTakenAsync(It.IsAny<string>())).ReturnsAsync(false);
        mockDatabase.Setup(x => x.GetNewUniqueId()).Returns(1);
        mockDatabase.Setup(x => x.SaveAsync(It.IsAny<IUser>()));

        IUserManager userManager = new UserManager(mockDatabase.Object);

        // Act.
        var resultTask = Assert.ThrowsAsync<ArgumentNullException>(() => userManager.RegisterUserAsync(null!, "TestPassword"));
        resultTask.Wait();
        var result = resultTask.Result;

        // Assert.
        Assert.Equal("Value cannot be null. (Parameter 'username')", result.Message);
    }

    [Fact]
    public void RegisterUserAsync_WithNullPassword_ThrowsArgumentNullException()
    {
        // Arrange.
        var mockDatabase = new Mock<IUserDatabaseOperator>();
        mockDatabase.Setup(x => x.IsUsernameTakenAsync(It.IsAny<string>())).ReturnsAsync(false);
        mockDatabase.Setup(x => x.GetNewUniqueId()).Returns(1);
        mockDatabase.Setup(x => x.SaveAsync(It.IsAny<IUser>()));

        IUserManager userManager = new UserManager(mockDatabase.Object);

        // Act.
        var resultTask = Assert.ThrowsAsync<ArgumentNullException>(() => userManager.RegisterUserAsync("TestUser", null!));
        resultTask.Wait();
        var result = resultTask.Result;

        // Assert.
        Assert.Equal("Value cannot be null. (Parameter 'password')", result.Message);
    }

    [Fact]
    public void RegisterUserAsync_WithInvalidUsernames_ThrowsInvalidUsernameException()
    {
        // Arrange.
        var mockDatabase = new Mock<IUserDatabaseOperator>();
        mockDatabase.Setup(x => x.IsUsernameTakenAsync(It.IsAny<string>())).ReturnsAsync(false);
        mockDatabase.Setup(x => x.GetNewUniqueId()).Returns(1);
        mockDatabase.Setup(x => x.SaveAsync(It.IsAny<IUser>()));

        IUserManager userManager = new UserManager(mockDatabase.Object);

        // Act.
        var resultTask1 = Assert.ThrowsAsync<InvalidUsernameException>(() => userManager.RegisterUserAsync("Te", "TestPassword"));
        resultTask1.Wait();
        var result1 = resultTask1.Result;

        var resultTask2 = Assert.ThrowsAsync<InvalidUsernameException>(() => userManager.RegisterUserAsync("This is a very long username that is longer than 32 characters.", "TestPassword"));
        resultTask2.Wait();
        var result2 = resultTask2.Result;

        // Assert.
        Assert.Equal("Username must be at least 3 characters long.", result1.Message);
        Assert.Equal("Username cannot be longer than 32 characters.", result2.Message);
    }

    [Fact]
    public void RegisterUserAsync_WithInvalidPasswords_ThrowsInvalidPasswordException()
    {
        // Arrange.
        var mockDatabase = new Mock<IUserDatabaseOperator>();
        mockDatabase.Setup(x => x.IsUsernameTakenAsync(It.IsAny<string>())).ReturnsAsync(false);
        mockDatabase.Setup(x => x.GetNewUniqueId()).Returns(1);
        mockDatabase.Setup(x => x.SaveAsync(It.IsAny<IUser>()));

        IUserManager userManager = new UserManager(mockDatabase.Object);

        // Act.
        var resultTask1 = Assert.ThrowsAsync<InvalidPasswordException>(() => userManager.RegisterUserAsync("TestUser", "Te"));
        resultTask1.Wait();
        var result1 = resultTask1.Result;

        var resultTask2 = Assert.ThrowsAsync<InvalidPasswordException>(() => userManager.RegisterUserAsync("TestUser", "This is a very, very, very long password that is just about as long as 72 characters."));
        resultTask2.Wait();
        var result2 = resultTask2.Result;

        // Assert.
        Assert.Equal("Password must be at least 8 characters long.", result1.Message);
        Assert.Equal("Password cannot be longer than 72 characters.", result2.Message);
    }

    [Fact]
    public void RegisterUserAsync_WithTakenUsername_ThrowsInvalidUsernameException()
    {
        // Arrange.
        var mockDatabase = new Mock<IUserDatabaseOperator>();
        mockDatabase.Setup(x => x.IsUsernameTakenAsync(It.IsAny<string>())).ReturnsAsync(true);
        mockDatabase.Setup(x => x.GetNewUniqueId()).Returns(1);
        mockDatabase.Setup(x => x.SaveAsync(It.IsAny<IUser>()));

        IUserManager userManager = new UserManager(mockDatabase.Object);

        // Act.
        var resultTask = Assert.ThrowsAsync<InvalidUsernameException>(() => userManager.RegisterUserAsync("TestUser", "TestPassword"));
        resultTask.Wait();
        var result = resultTask.Result;

        // Assert.
        Assert.Equal("Username is already taken.", result.Message);
    }

    [Fact]
    public void LoadUserAsync_WithValidUsername_ReturnsUser()
    {
        // Arrange.
        var mockUser = new Mock<IUser>();
        mockUser.Setup(u => u.Id).Returns(1);
        mockUser.Setup(u => u.Username).Returns("TestUser");
        mockUser.Setup(u => u.Password).Returns("TestPassword");

        var mockDatabase = new Mock<IUserDatabaseOperator>();
        mockDatabase.Setup(x => x.GetByUsernameAsync(It.IsAny<string>())).ReturnsAsync(mockUser.Object);

        IUserManager userManager = new UserManager(mockDatabase.Object);

        // Act.
        var result = userManager.LoadUserAsync("TestUser").Result;

        // Assert.
        Assert.NotNull(result);
    }

    [Fact]
    public void LoadUserAsync_WithNullUsername_ThrowsArgumentNullException()
    {
        // Arrange.
        var mockDatabase = new Mock<IUserDatabaseOperator>();
        mockDatabase.Setup(x => x.GetByUsernameAsync(It.IsAny<string>())).ReturnsAsync((IUser?)null);

        IUserManager userManager = new UserManager(mockDatabase.Object);

        // Act.
        var resultTask = Assert.ThrowsAsync<ArgumentException>(() => userManager.LoadUserAsync(null!));
        resultTask.Wait();
        var result = resultTask.Result;

        // Assert.
        Assert.Equal("Username cannot be null or whitespace. (Parameter 'username')", result.Message);
    }

    [Fact]
    public void LoadUserAsync_WithNonExistentUsername_ThrowsUserNotFoundException()
    {
        // Arrange.
        var mockDatabase = new Mock<IUserDatabaseOperator>();
        mockDatabase.Setup(x => x.GetByUsernameAsync(It.IsAny<string>())).ReturnsAsync((IUser?)null);

        IUserManager userManager = new UserManager(mockDatabase.Object);

        // Act.
        var resultTask = Assert.ThrowsAsync<UserNotFoundException>(() => userManager.LoadUserAsync("TestUser"));
        resultTask.Wait();
        var result = resultTask.Result;

        // Assert.
        Assert.Equal("User with username 'TestUser' not found.", result.Message);
    }

    [Fact]
    public void MakeAdminAsync_WithValidUser_MakesUserAdmin()
    {
        // Arrange.
        var mockUser = new Mock<IUser>();
        mockUser.Setup(u => u.Id).Returns(1);
        mockUser.Setup(u => u.Username).Returns("TestUser");
        mockUser.Setup(u => u.Password).Returns("TestPassword");
        mockUser.SetupProperty(u => u.IsAdmin);
        mockUser.Object.IsAdmin = false;

        var mockDatabase = new Mock<IUserDatabaseOperator>();
        mockDatabase.Setup(x => x.GetByUsernameAsync(It.IsAny<string>())).ReturnsAsync(mockUser.Object);
        mockDatabase.Setup(x => x.OverwriteAsync(It.IsAny<IUser>()));

        IUserManager userManager = new UserManager(mockDatabase.Object);
        userManager.RegisterUserAsync("TestUser", "TestPassword").Wait();

        // Act.
        userManager.MakeAdminAsync(mockUser.Object).Wait();
        var resultUser = userManager.LoadUserAsync("TestUser").Result;

        // Assert.
        Assert.True(resultUser.IsAdmin);
    }

    [Fact]
    public void MakeAdminAsync_WithNullUser_ThrowsArgumentNullException()
    {
        // Arrange.
        var mockDatabase = new Mock<IUserDatabaseOperator>();
        mockDatabase.Setup(x => x.GetByUsernameAsync(It.IsAny<string>())).ReturnsAsync((IUser?)null);

        IUserManager userManager = new UserManager(mockDatabase.Object);

        // Act.
        var resultTask = Assert.ThrowsAsync<ArgumentNullException>(() => userManager.MakeAdminAsync(null!));
        resultTask.Wait();
        var result = resultTask.Result;

        // Assert.
        Assert.Equal("Value cannot be null. (Parameter 'userToPromote')", result.Message);
    }

    [Fact]
    public void MakeAdminAsync_WithNonExistentUser_ThrowsUserNotFoundException()
    {
        // Arrange.
        var mockUser = new Mock<IUser>();
        mockUser.Setup(u => u.Id).Returns(1);
        mockUser.Setup(u => u.Username).Returns("TestUser");
        mockUser.Setup(u => u.Password).Returns("TestPassword");
        mockUser.SetupProperty(u => u.IsAdmin);
        mockUser.Object.IsAdmin = false;

        var mockDatabase = new Mock<IUserDatabaseOperator>();
        mockDatabase.Setup(x => x.GetByUsernameAsync(It.IsAny<string>())).ReturnsAsync(mockUser.Object);
        mockDatabase.Setup(x => x.OverwriteAsync(It.IsAny<IUser>())).ThrowsAsync(new UserNotFoundException("TestUser"));

        IUserManager userManager = new UserManager(mockDatabase.Object);

        // Act.
        var resultTask = Assert.ThrowsAsync<UserNotFoundException>(() => userManager.MakeAdminAsync(mockUser.Object));
        resultTask.Wait();
        var result = resultTask.Result;

        // Assert.
        Assert.Equal("User with username 'TestUser' not found.", result.Message);
    }

    [Fact]
    public void DemoteAdminAsync_WithValidUser_DemotesUser()
    {
        // Arrange.
        var mockUser = new Mock<IUser>();
        mockUser.Setup(u => u.Id).Returns(1);
        mockUser.Setup(u => u.Username).Returns("TestUser");
        mockUser.Setup(u => u.Password).Returns("TestPassword");
        mockUser.SetupProperty(u => u.IsAdmin);
        mockUser.Object.IsAdmin = true;

        var mockDatabase = new Mock<IUserDatabaseOperator>();
        mockDatabase.Setup(x => x.GetByUsernameAsync(It.IsAny<string>())).ReturnsAsync(mockUser.Object);
        mockDatabase.Setup(x => x.OverwriteAsync(It.IsAny<IUser>()));

        IUserManager userManager = new UserManager(mockDatabase.Object);
        userManager.RegisterUserAsync("TestUser", "TestPassword").Wait();

        // Act.
        userManager.DemoteAdminAsync(mockUser.Object).Wait();
        var resultUser = userManager.LoadUserAsync("TestUser").Result;

        // Assert.
        Assert.False(resultUser.IsAdmin);
    }

    [Fact]
    public void DemoteAdminAsync_WithNullUser_ThrowsArgumentNullException()
    {
        // Arrange.
        var mockDatabase = new Mock<IUserDatabaseOperator>();
        mockDatabase.Setup(x => x.GetByUsernameAsync(It.IsAny<string>())).ReturnsAsync((IUser?)null);

        IUserManager userManager = new UserManager(mockDatabase.Object);

        // Act.
        var resultTask = Assert.ThrowsAsync<ArgumentNullException>(() => userManager.DemoteAdminAsync(null!));
        resultTask.Wait();
        var result = resultTask.Result;

        // Assert.
        Assert.Equal("Value cannot be null. (Parameter 'userToDemote')", result.Message);
    }

    [Fact]
    public void DemoteAdminAsync_WithNonExistentUser_ThrowsUserNotFoundException()
    {
        // Arrange.
        var mockUser = new Mock<IUser>();
        mockUser.Setup(u => u.Id).Returns(1);
        mockUser.Setup(u => u.Username).Returns("TestUser");
        mockUser.Setup(u => u.Password).Returns("TestPassword");
        mockUser.SetupProperty(u => u.IsAdmin);
        mockUser.Object.IsAdmin = true;

        var mockDatabase = new Mock<IUserDatabaseOperator>();
        mockDatabase.Setup(x => x.GetByUsernameAsync(It.IsAny<string>())).ReturnsAsync(mockUser.Object);
        mockDatabase.Setup(x => x.OverwriteAsync(It.IsAny<IUser>())).ThrowsAsync(new UserNotFoundException("TestUser"));

        IUserManager userManager = new UserManager(mockDatabase.Object);

        // Act.
        var resultTask = Assert.ThrowsAsync<UserNotFoundException>(() => userManager.DemoteAdminAsync(mockUser.Object));
        resultTask.Wait();
        var result = resultTask.Result;

        // Assert.
        Assert.Equal("User with username 'TestUser' not found.", result.Message);
    }
}