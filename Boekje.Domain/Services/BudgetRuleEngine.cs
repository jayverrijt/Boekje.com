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
        _rules = rules;
    }

    public BudgetAdvice Generate(
        Budget budget)
    {
        if (budget == null)
        {
            throw new Exception(
                "Budget is required.");
        }

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