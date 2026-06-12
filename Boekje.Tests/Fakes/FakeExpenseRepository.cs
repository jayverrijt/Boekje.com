using Boekje.Domain.Entities;
using Boekje.Domain.Interfaces;

namespace Boekje.Tests.Fakes;

public class FakeExpenseRepository
    : IExpenseRepository
{
    public List<Expense> Expenses
        = new();

    public List<Expense> GetByBudget(
        int budgetId)
    {
        return Expenses;
    }

    public void Insert(
        int budgetId,
        Expense expense)
    {
        Expenses.Add(expense);
    }

    public void DeleteByBudget(
        int budgetId)
    {
        Expenses.Clear();
    }
}