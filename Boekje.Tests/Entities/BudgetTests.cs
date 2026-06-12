using Boekje.Domain.Entities;

namespace Boekje.Tests.Entities;

public class BudgetTests
{
    [Fact]
    public void Constructor_SetsValues()
    {
        var budget =
            new Budget(1, 2500);

        Assert.Equal(1, budget.UserId);
        Assert.Equal(2500, budget.Income);
    }

    [Fact]
    public void Constructor_NegativeIncome_Throws()
    {
        Assert.Throws<Exception>(
            () => new Budget(1, -1));
    }

    [Fact]
    public void AddExpense_AddsExpense()
    {
        var budget =
            new Budget(1, 1000);

        var expense =
            new Expense(
                "Food",
                100,
                "Pizza");

        budget.AddExpense(expense);

        Assert.Single(
            budget.Expenses);
    }

    [Fact]
    public void AddExpense_Duplicate_Throws()
    {
        var budget =
            new Budget(1, 1000);

        var expense =
            new Expense(
                "Food",
                100,
                "Pizza");

        budget.AddExpense(expense);

        Assert.Throws<Exception>(
            () => budget.AddExpense(expense));
    }

    [Fact]
    public void IsOverBudget_ReturnsTrue()
    {
        var budget =
            new Budget(1, 100);

        budget.AddExpense(
            new Expense(
                "Food",
                200,
                "Pizza"));

        Assert.True(
            budget.IsOverBudget());
    }
}