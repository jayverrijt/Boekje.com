using Boekje.Domain.Entities;
using Boekje.Domain.Interfaces;

namespace Boekje.Domain.Services;

public class BudgetRuleEngine
{
    private readonly IEnumerable<IBudgetRule>
        _rules;

    public BudgetRuleEngine(
        IEnumerable<IBudgetRule> rules)
    {
        _rules = rules
                 ?? throw new ArgumentNullException(
                     nameof(rules));
    }

    public BudgetAdvice Generate(
        Budget budget)
    {
        ArgumentNullException.ThrowIfNull(
            budget);

        var advice =
            new BudgetAdvice(
                budget.Income * 0.30m,
                budget.Income * 0.10m);

        foreach (var rule in _rules)
        {
            rule.Evaluate(
                budget,
                advice);
        }

        if (budget.IsOverBudget())
        {
            advice.AddWarning(
                "Budget is negatief.");
        }

        return advice;
    }
}