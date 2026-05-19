using Boekje.Domain.Entities;

namespace Boekje.Tests;

public class ExpenseTests
{
    [Fact]
    public void Constructor_SetsPropertiesCorrectly()
    {
        // Act

        var expense =
            new Expense(
                "Food",
                25m,
                "Lunch");

        // Assert

        Assert.Equal(
            "Food",
            expense.Type);

        Assert.Equal(
            25m,
            expense.Amount);

        Assert.Equal(
            "Lunch",
            expense.Name);
    }

    [Fact]
    public void Constructor_ThrowsException_WhenAmountNegative()
    {
        // Act & Assert

        Assert.Throws<Exception>(() =>
            new Expense(
                "Food",
                -10m,
                "Invalid"));
    }
}