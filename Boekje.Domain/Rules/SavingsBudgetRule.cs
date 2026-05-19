using Boekje.Domain.Entities;
using Boekje.Domain.Interfaces;

namespace Boekje.Domain.Rules;

public class SavingsBudgetRule
    : IBudgetRule
{
    public void Evaluate(
        Budget budget,
        BudgetAdvice advice)
    {
        if (budget.GetTotalSavings()
            < advice.RecommendedMinimumSavings)
        {
            advice.AddWarning(
                "Spaargeld is lager dan aanbevolen.");
        }
    }
}