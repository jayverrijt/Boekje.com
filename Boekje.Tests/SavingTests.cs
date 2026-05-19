using Boekje.Domain.Entities;

namespace Boekje.Tests;

public class SavingTests
{
    [Fact]
    public void Constructor_SetsPropertiesCorrectly()
    {
        // Act

        var saving =
            new Saving(
                100m,
                "Emergency Fund");

        // Assert

        Assert.Equal(
            100m,
            saving.Amount);

        Assert.Equal(
            "Emergency Fund",
            saving.Name);
    }

    [Fact]
    public void Constructor_ThrowsException_WhenAmountNegative()
    {
        // Act & Assert

        Assert.Throws<Exception>(() =>
            new Saving(
                -50m,
                "Invalid"));
    }
}