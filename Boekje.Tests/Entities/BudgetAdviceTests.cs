using Boekje.Domain.Entities;

namespace Boekje.Tests.Entities;

public class BudgetAdviceTests
{
    [Fact]
    public void AddWarning_AddsWarning()
    {
        var advice =
            new BudgetAdvice(
                500,
                100);

        advice.AddWarning(
            "Test");

        Assert.True(
            advice.HasWarnings());
    }

    [Fact]
    public void EmptyWarning_Throws()
    {
        var advice =
            new BudgetAdvice(
                500,
                100);

        Assert.Throws<Exception>(
            () =>
                advice.AddWarning(""));
    }
}