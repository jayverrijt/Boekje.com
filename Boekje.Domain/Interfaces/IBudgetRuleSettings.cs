namespace Boekje.Domain.Interfaces;

public interface IBudgetRuleSettings
{
    decimal MaxRentPercentage { get; }

    decimal MinSavingsPercentage { get; }
}