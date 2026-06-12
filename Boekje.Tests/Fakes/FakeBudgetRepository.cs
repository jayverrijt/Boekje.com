using Boekje.Domain.Entities;
using Boekje.Domain.Interfaces;

namespace Boekje.Tests.Fakes;

public class FakeBudgetRepository
    : IBudgetRepository
{
    public List<Budget> Budgets { get; }
        = new();

    public Budget? GetByUser(
        int userId)
    {
        return Budgets
            .FirstOrDefault(
                x => x.UserId == userId);
    }

    public int CreateBudget(
        Budget budget)
    {
        budget.SetId(
            Budgets.Count + 1);

        Budgets.Add(
            budget);

        return budget.Id;
    }

    public void InsertExpenses(
        int budgetId,
        IReadOnlyList<Expense> expenses)
    {
    }

    public void InsertSavings(
        int budgetId,
        IReadOnlyList<Saving> savings)
    {
    }

    public void InsertCategories(
        int budgetId,
        Dictionary<string, decimal>
            categories)
    {
    }

    public Dictionary<string, decimal>
        GetCategories(
            int budgetId)
    {
        return new();
    }

    public void DeleteByBudgetId(
        int budgetId)
    {
        Budgets.RemoveAll(
            x => x.Id == budgetId);
    }

    public void DeleteByUserId(
        int userId)
    {
        Budgets.RemoveAll(
            x => x.UserId == userId);
    }
}