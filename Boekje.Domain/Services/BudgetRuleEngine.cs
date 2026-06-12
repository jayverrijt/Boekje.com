using Boekje.Domain.Entities;
using Boekje.Domain.Interfaces;

namespace Boekje.Domain.Services;

public class BudgetRuleEngine
{
    private readonly IEnumerable<IBudgetRule>
        _rules;

    private readonly IBudgetRuleSettings
        _settings;

    public BudgetRuleEngine(
        IEnumerable<IBudgetRule> rules,
        IBudgetRuleSettings settings)
    {
        ArgumentNullException.ThrowIfNull(
            rules);

        ArgumentNullException.ThrowIfNull(
            settings);

        _rules = rules;
        _settings = settings;
    }

    public BudgetAdvice Generate(
        Budget budget)
    {
        ArgumentNullException.ThrowIfNull(
            budget);

        var advice =
            new BudgetAdvice(
                budget.Income *
                _settings.MaxRentPercentage,

                budget.Income *
                _settings.MinSavingsPercentage);

        foreach (var rule in _rules)
        {
            rule.Evaluate(
                budget,
                advice);
        }

        if (budget.IsOverBudget())
        {
            advice.AddWarning(
                "Budget is in de min (Negatief).");
        }

        return advice;
    }
}