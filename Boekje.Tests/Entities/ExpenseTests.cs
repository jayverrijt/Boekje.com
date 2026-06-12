using Boekje.Domain.Entities;

namespace Boekje.Tests.Entities;

public class ExpenseTests
{
    [Fact]
    public void Constructor_SetsProperties()
    {
        var expense =
            new Expense(
                "Food",
                100,
                "Pizza");

        Assert.Equal(
            "Food",
            expense.Type);

        Assert.Equal(
            100,
            expense.Amount);

        Assert.Equal(
            "Pizza",
            expense.Name);
    }

    [Fact]
    public void Constructor_EmptyType_Throws()
    {
        Assert.Throws<Exception>(() =>
        {
            new Expense(
                "",
                100,
                "Pizza");
        });
    }

    [Fact]
    public void ChangeType_UpdatesType()
    {
        var expense =
            new Expense(
                "Food",
                100,
                "Pizza");

        expense.ChangeType(
            "Transport");

        Assert.Equal(
            "Transport",
            expense.Type);
    }

    [Fact]
    public void ChangeType_EmptyType_Throws()
    {
        var expense =
            new Expense(
                "Food",
                100,
                "Pizza");

        Assert.Throws<Exception>(() =>
        {
            expense.ChangeType("");
        });
    }

    [Fact]
    public void Rename_ChangesName()
    {
        var expense =
            new Expense(
                "Food",
                100,
                "Pizza");

        expense.Rename(
            "Burger");

        Assert.Equal(
            "Burger",
            expense.Name);
    }

    [Fact]
    public void Rename_EmptyName_Throws()
    {
        var expense =
            new Expense(
                "Food",
                100,
                "Pizza");

        Assert.Throws<Exception>(() =>
        {
            expense.Rename("");
        });
    }

    [Fact]
    public void UpdateAmount_ChangesAmount()
    {
        var expense =
            new Expense(
                "Food",
                100,
                "Pizza");

        expense.UpdateAmount(
            250);

        Assert.Equal(
            250,
            expense.Amount);
    }

    [Fact]
    public void UpdateAmount_NegativeAmount_Throws()
    {
        var expense =
            new Expense(
                "Food",
                100,
                "Pizza");

        Assert.Throws<Exception>(() =>
        {
            expense.UpdateAmount(-1);
        });
    }
}