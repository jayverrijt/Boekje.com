    using Boekje.Domain.Entities;
    using Boekje.Domain.Interfaces;

    namespace Boekje.Domain.Rules;

    public class RentBudgetRule : IBudgetRule
    {
        public void Evaluate(
            Budget budget,
            BudgetAdvice advice)
        {
            foreach (var expense
                     in budget.Expenses)
            {
                if (expense.Type == "Huur")
                {
                    if (expense.Amount >
                        advice.RecommendedMaxRent)
                    {
                        advice.AddWarning(
                            "Huur is hoger dan aanbevolen.");
                    }
                }
            }
        }
    }