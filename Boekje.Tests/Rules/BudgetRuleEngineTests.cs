using Boekje.Domain.Entities;
using Boekje.Domain.Interfaces;
using Boekje.Domain.Services;
using Boekje.Tests.Fakes;

namespace Boekje.Tests.Rules;

public class BudgetRuleEngineTests
{
    [Fact]
    public void Generate_ReturnsAdvice()
    {
        var rules =
            new List<IBudgetRule>();

        var settings =
            new FakeBudgetRuleSettings();

        var engine =
            new BudgetRuleEngine(
                rules,
                settings);

        var budget =
            new Budget(
                1,
                3000);

        var advice =
            engine.Generate(
                budget);

        Assert.NotNull(
            advice);

        Assert.Equal(
            900,
            advice.RecommendedMaxRent);

        Assert.Equal(
            300,
            advice.RecommendedMinimumSavings);
    }

    [Fact]
    public void Generate_OverBudget_AddsWarning()
    {
        var rules =
            new List<IBudgetRule>();

        var settings =
            new FakeBudgetRuleSettings();

        var engine =
            new BudgetRuleEngine(
                rules,
                settings);

        var budget =
            new Budget(
                1,
                100);

        budget.AddExpense(
            new Expense(
                "Food",
                200,
                "Pizza"));

        var advice =
            engine.Generate(
                budget);

        Assert.True(
            advice.HasWarnings());

        Assert.Contains(
            "Budget is negatief.",
            advice.Warnings);
    }

    [Fact]
    public void Constructor_NullRules_Throws()
    {
        var settings =
            new FakeBudgetRuleSettings();

        Assert.Throws<ArgumentNullException>(
            () =>
                new BudgetRuleEngine(
                    null!,
                    settings));
    }

    [Fact]
    public void Generate_NullBudget_Throws()
    {
        var settings =
            new FakeBudgetRuleSettings();

        var engine =
            new BudgetRuleEngine(
                new List<IBudgetRule>(),
                settings);

        Assert.Throws<ArgumentNullException>(
            () =>
                engine.Generate(
                    null!));
    }
}