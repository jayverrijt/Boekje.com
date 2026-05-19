using Boekje.Domain.Entities;

namespace Boekje.Domain.Interfaces;

public interface IExpenseRepository
{
    List<Expense> GetByBudget(
        int budgetId);

    void Insert(
        int budgetId,
        Expense expense);

    void DeleteByBudget(
        int budgetId);
}