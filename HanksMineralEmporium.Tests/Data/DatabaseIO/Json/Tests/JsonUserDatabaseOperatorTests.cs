namespace HanksMineralEmporium.Data.DatabaseIO.Json.Tests;

public class JsonUserDatabaseOperatorTests
{
    private readonly string _databasePath = Path.Combine(Environment.CurrentDirectory, "Data", "Database", "Users.json");

    [Fact]
    public void GetByUsernameAsync_ExistingUser_ReturnsUser()
    {
        // Arrange
        var userDatabaseOperator = new JsonUserDatabaseOperator();

        // Act
        var result = userDatabaseOperator.GetByUsernameAsync("admin").Result;

        // Assert
        Assert.NotNull(result);
        Assert.Equal("admin", result.Username);

        // Cleanup.
        File.Delete(_databasePath);
    }

    [Fact]
    public void GetByUsernameAsync_WithNullandEmptyUsernames_ThrowsArgumentException()
    {
        // Arrange
        var userDatabaseOperator = new JsonUserDatabaseOperator();

        // Act
        var resultTask1 = Assert.ThrowsAsync<ArgumentException>(() => userDatabaseOperator.GetByUsernameAsync(null!));
        resultTask1.Wait();
        var result1 = resultTask1.Result;
        var resultTask2 = Assert.ThrowsAsync<ArgumentException>(() => userDatabaseOperator.GetByUsernameAsync(string.Empty));
        resultTask2.Wait();
        var result2 = resultTask2.Result;

        // Assert
        Assert.Equal("Username cannot be null or whitespace. (Parameter 'username')", result1.Message);
        Assert.Equal("Username cannot be null or whitespace. (Parameter 'username')", result2.Message);

        // Cleanup.
        File.Delete(_databasePath);
    }

    [Fact]
    public void GetByUsernameAsync_WithNonExistingUser_ReturnsNull()
    {
        // Arrange
        var userDatabaseOperator = new JsonUserDatabaseOperator();

        // Act
        var result = userDatabaseOperator.GetByUsernameAsync("nonexistinguser").Result;

        // Assert
        Assert.Null(result);

        // Cleanup.
        File.Delete(_databasePath);
    }

    [Fact]
    public void IsUsernameTakenAsync_WithExistingUser_ReturnsTrue()
    {
        // Arrange
        var userDatabaseOperator = new JsonUserDatabaseOperator();

        // Act
        var result = userDatabaseOperator.IsUsernameTakenAsync("admin").Result;

        // Assert
        Assert.True(result);

        // Cleanup.
        File.Delete(_databasePath);
    }

    [Fact]
    public void IsUsernameTakenAsync_WithNonExistingUser_ReturnsFalse()
    {
        // Arrange
        var userDatabaseOperator = new JsonUserDatabaseOperator();

        // Act
        var result = userDatabaseOperator.IsUsernameTakenAsync("nonexistinguser").Result;

        // Assert
        Assert.False(result);

        // Cleanup.
        File.Delete(_databasePath);
    }

    [Fact]
    public void IsUserNameTakenAsync_WithNullandEmptyUsernames_ThrowsArgumentException()
    {
        // Arrange
        var userDatabaseOperator = new JsonUserDatabaseOperator();

        // Act
        var resultTask1 = Assert.ThrowsAsync<ArgumentException>(() => userDatabaseOperator.IsUsernameTakenAsync(null!));
        resultTask1.Wait();
        var result1 = resultTask1.Result;
        var resultTask2 = Assert.ThrowsAsync<ArgumentException>(() => userDatabaseOperator.IsUsernameTakenAsync(string.Empty));
        resultTask2.Wait();
        var result2 = resultTask2.Result;

        // Assert
        Assert.Equal("Username cannot be null or whitespace. (Parameter 'username')", result1.Message);
        Assert.Equal("Username cannot be null or whitespace. (Parameter 'username')", result2.Message);

        // Cleanup.
        File.Delete(_databasePath);
    }
}