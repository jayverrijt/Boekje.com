using Boekje.Domain.Entities;
using Boekje.Domain.Rules;

namespace Boekje.Tests.Rules;

public class RentBudgetRuleTests
{
    [Fact]
    public void RentAboveRecommendation_AddsWarning()
    {
        var budget =
            new Budget(
                1,
                2000);

        budget.AddExpense(
            new Expense(
                "Huur",
                1000,
                "Apartment"));

        var advice =
            new BudgetAdvice(
                500,
                200);

        var rule =
            new RentBudgetRule();

        rule.Evaluate(
            budget,
            advice);

        Assert.True(
            advice.HasWarnings());
    }

    [Fact]
    public void RentBelowRecommendation_NoWarning()
    {
        var budget =
            new Budget(
                1,
                2000);

        budget.AddExpense(
            new Expense(
                "Huur",
                300,
                "Apartment"));

        var advice =
            new BudgetAdvice(
                500,
                200);

        var rule =
            new RentBudgetRule();

        rule.Evaluate(
            budget,
            advice);

        Assert.False(
            advice.HasWarnings());
    }

    [Fact]
    public void NonRentExpense_NoWarning()
    {
        var budget =
            new Budget(
                1,
                2000);

        budget.AddExpense(
            new Expense(
                "Food",
                1000,
                "Pizza"));

        var advice =
            new BudgetAdvice(
                500,
                200);

        var rule =
            new RentBudgetRule();

        rule.Evaluate(
            budget,
            advice);

        Assert.False(
            advice.HasWarnings());
    }
}