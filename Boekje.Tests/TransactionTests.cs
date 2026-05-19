using Boekje.Domain.Entities;

namespace Boekje.Tests;

public class TransactionTests
{
    [Fact]
    public void CreateExpense_SetsCorrectType()
    {
        // Act

        var transaction =
            Transaction.CreateExpense(
                1,
                100m,
                "Food",
                "McDonalds");

        // Assert

        Assert.Equal(
            "expense",
            transaction.Type);
    }

    [Fact]
    public void CreateIncome_SetsCorrectType()
    {
        // Act

        var transaction =
            Transaction.CreateIncome(
                1,
                2000m,
                "Salary",
                "Job");

        // Assert

        Assert.Equal(
            "income",
            transaction.Type);
    }

    [Fact]
    public void IsExpense_ReturnsTrue_ForExpense()
    {
        // Arrange

        var transaction =
            Transaction.CreateExpense(
                1,
                50m,
                "Food",
                "Lunch");

        // Act & Assert

        Assert.True(
            transaction.IsExpense());
    }

    [Fact]
    public void IsIncome_ReturnsTrue_ForIncome()
    {
        // Arrange

        var transaction =
            Transaction.CreateIncome(
                1,
                3000m,
                "Salary",
                "Monthly");

        // Act & Assert

        Assert.True(
            transaction.IsIncome());
    }

    [Fact]
    public void Constructor_ThrowsException_WhenAmountNegative()
    {
        // Act & Assert

        Assert.Throws<Exception>(() =>
            new Transaction(
                1,
                -100m,
                "expense",
                "Food",
                "Invalid"));
    }
}