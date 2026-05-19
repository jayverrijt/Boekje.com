using Boekje.Domain.Entities;
using Boekje.Domain.Services;

namespace Boekje.Tests;

public class BudgetRulesServiceTests
{
    private readonly BudgetRulesService
        _service;

    public BudgetRulesServiceTests()
    {
        _service =
            new BudgetRulesService();
    }

    [Fact]
    public void GenerateAdvice_ReturnsCorrectRentRecommendation()
    {
        // Arrange

        var budget = new Budget(
            1,
            2000m);

        // Act

        var advice =
            _service.GenerateAdvice(
                budget);

        // Assert

        Assert.Equal(
            600m,
            advice.RecommendedMaxRent);
    }

    [Fact]
    public void GenerateAdvice_AddsWarning_WhenRentTooHigh()
    {
        // Arrange

        var budget = new Budget(
            1,
            2000m);

        var expense = new Expense(
            "Huur",
            900m,
            "Appartement");

        budget.AddExpense(expense);

        // Act

        var advice =
            _service.GenerateAdvice(
                budget);

        // Assert

        Assert.Contains(
            advice.Warnings,
            x => x.Contains("Huur"));
    }

    [Fact]
    public void GenerateAdvice_AddsWarning_WhenSavingsTooLow()
    {
        // Arrange

        var budget = new Budget(
            1,
            3000m);

        var saving = new Saving(
            50m,
            "Noodfonds");

        budget.AddSaving(saving);

        // Act

        var advice =
            _service.GenerateAdvice(
                budget);

        // Assert

        Assert.Contains(
            advice.Warnings,
            x => x.Contains("Spaargeld"));
    }

    [Fact]
    public void GenerateAdvice_AddsWarning_WhenBudgetNegative()
    {
        // Arrange

        var budget = new Budget(
            1,
            1000m);

        budget.AddExpense(
            new Expense(
                "Huur",
                1200m,
                "Appartement"));

        // Act

        var advice =
            _service.GenerateAdvice(
                budget);

        // Assert

        Assert.Contains(
            advice.Warnings,
            x => x.Contains("Budget"));
    }
}