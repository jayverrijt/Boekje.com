using Boekje.Domain.Entities;

namespace Boekje.Tests.Entities;

public class UserTests
{
    [Fact]
    public void Constructor_SetsProperties()
    {
        var user =
            new User(
                "John",
                "john@test.com",
                "hash");

        Assert.Equal(
            "John",
            user.Name);
    }

    [Fact]
    public void ChangeName_UpdatesName()
    {
        var user =
            new User(
                "John",
                "john@test.com",
                "hash");

        user.ChangeName(
            "Peter");

        Assert.Equal(
            "Peter",
            user.Name);
    }

    [Fact]
    public void EmptyName_Throws()
    {
        Assert.Throws<Exception>(
            () => new User(
                "",
                "mail",
                "hash"));
    }
}