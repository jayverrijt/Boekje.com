using Boekje.Domain.Entities;

namespace Boekje.Tests.Entities;

public class SavingTests
{
    [Fact]
    public void Constructor_SetsProperties()
    {
        var saving =
            new Saving(
                500,
                "Emergency Fund");

        Assert.Equal(
            500,
            saving.Amount);

        Assert.Equal(
            "Emergency Fund",
            saving.Name);
    }

    [Fact]
    public void Constructor_NegativeAmount_Throws()
    {
        Assert.Throws<Exception>(() =>
        {
            new Saving(
                -1,
                "Emergency Fund");
        });
    }

    [Fact]
    public void Constructor_EmptyName_Throws()
    {
        Assert.Throws<Exception>(() =>
        {
            new Saving(
                100,
                "");
        });
    }

    [Fact]
    public void Rename_ChangesName()
    {
        var saving =
            new Saving(
                500,
                "Emergency Fund");

        saving.Rename(
            "Vacation");

        Assert.Equal(
            "Vacation",
            saving.Name);
    }

    [Fact]
    public void Rename_EmptyName_Throws()
    {
        var saving =
            new Saving(
                500,
                "Emergency Fund");

        Assert.Throws<Exception>(() =>
        {
            saving.Rename("");
        });
    }

    [Fact]
    public void UpdateAmount_ChangesAmount()
    {
        var saving =
            new Saving(
                500,
                "Emergency Fund");

        saving.UpdateAmount(
            1000);

        Assert.Equal(
            1000,
            saving.Amount);
    }

    [Fact]
    public void UpdateAmount_NegativeAmount_Throws()
    {
        var saving =
            new Saving(
                500,
                "Emergency Fund");

        Assert.Throws<Exception>(() =>
        {
            saving.UpdateAmount(-100);
        });
    }
}