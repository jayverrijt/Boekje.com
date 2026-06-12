using Boekje.Domain.Interfaces;

namespace Boekje.Tests.Fakes;

public class FakeBudgetRuleSettings
    : IBudgetRuleSettings
{
    public decimal MaxRentPercentage =>
        0.30m;

    public decimal MinSavingsPercentage =>
        0.10m;
}