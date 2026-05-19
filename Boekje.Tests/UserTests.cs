using Boekje.Domain.Entities;

namespace Boekje.Tests;

public class UserTests
{
    [Fact]
    public void Constructor_SetsPropertiesCorrectly()
    {
        // Act

        var user =
            new User(
                "Jay",
                "jay@test.com",
                "hashedPassword");

        // Assert

        Assert.Equal(
            "Jay",
            user.Name);

        Assert.Equal(
            "jay@test.com",
            user.Email);
    }

    [Fact]
    public void Constructor_ThrowsException_WhenNameEmpty()
    {
        // Act & Assert

        Assert.Throws<Exception>(() =>
            new User(
                "",
                "test@test.com",
                "password"));
    }

    [Fact]
    public void ChangePassword_ChangesPasswordHash()
    {
        // Arrange

        var user =
            new User(
                "Jay",
                "jay@test.com",
                "oldHash");

        // Act

        user.ChangePassword(
            "newHash");

        // Assert

        Assert.Equal(
            "newHash",
            user.PasswordHash);
    }
}