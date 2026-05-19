using Boekje.Domain.Entities;

namespace Boekje.Tests;

public class BudgetTests
{
    [Fact]
    public void AddExpense_IncreasesTotalExpenses()
    {
        // Arrange

        var budget =
            new Budget(
                1,
                3000m);

        var expense =
            new Expense(
                "Huur",
                1000m,
                "Appartement");

        // Act

        budget.AddExpense(
            expense);

        // Assert

        Assert.Equal(
            1000m,
            budget.GetTotalExpenses());
    }

    [Fact]
    public void AddSaving_IncreasesTotalSavings()
    {
        // Arrange

        var budget =
            new Budget(
                1,
                3000m);

        var saving =
            new Saving(
                500m,
                "Noodfonds");

        // Act

        budget.AddSaving(
            saving);

        // Assert

        Assert.Equal(
            500m,
            budget.GetTotalSavings());
    }

    [Fact]
    public void GetRemaining_ReturnsCorrectAmount()
    {
        // Arrange

        var budget =
            new Budget(
                1,
                3000m);

        budget.AddExpense(
            new Expense(
                "Huur",
                1000m,
                "Appartement"));

        budget.AddSaving(
            new Saving(
                500m,
                "Spaargeld"));

        // Act

        var remaining =
            budget.GetRemaining();

        // Assert

        Assert.Equal(
            1500m,
            remaining);
    }

    [Fact]
    public void IsOverBudget_ReturnsTrue_WhenNegative()
    {
        // Arrange

        var budget =
            new Budget(
                1,
                1000m);

        budget.AddExpense(
            new Expense(
                "Huur",
                1500m,
                "Appartement"));

        // Act

        var result =
            budget.IsOverBudget();

        // Assert

        Assert.True(result);
    }

    [Fact]
    public void Constructor_ThrowsException_WhenIncomeNegative()
    {
        // Act & Assert

        Assert.Throws<Exception>(() =>
            new Budget(
                1,
                -100m));
    }
}