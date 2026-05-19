using Boekje.Domain.Entities;

namespace Boekje.Domain.Interfaces;

public interface IBudgetRule
{
    void Evaluate(
        Budget budget,
        BudgetAdvice advice);
}