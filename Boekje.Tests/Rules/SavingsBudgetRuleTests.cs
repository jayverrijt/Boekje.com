using Boekje.Domain.Entities;
using Boekje.Domain.Rules;

namespace Boekje.Tests.Rules;

public class SavingsBudgetRuleTests
{
    [Fact]
    public void SavingsBelowRecommendation_AddsWarning()
    {
        var budget =
            new Budget(
                1,
                3000);

        budget.AddSaving(
            new Saving(
                100,
                "Savings"));

        var advice =
            new BudgetAdvice(
                900,
                500);

        var rule =
            new SavingsBudgetRule();

        rule.Evaluate(
            budget,
            advice);

        Assert.True(
            advice.HasWarnings());
    }

    [Fact]
    public void SavingsAboveRecommendation_NoWarning()
    {
        var budget =
            new Budget(
                1,
                3000);

        budget.AddSaving(
            new Saving(
                1000,
                "Savings"));

        var advice =
            new BudgetAdvice(
                900,
                500);

        var rule =
            new SavingsBudgetRule();

        rule.Evaluate(
            budget,
            advice);

        Assert.False(
            advice.HasWarnings());
    }
}