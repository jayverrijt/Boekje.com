using Boekje.Domain.Entities;

namespace Boekje.Domain.Interfaces;

public interface IBudgetRepository
{
    Budget? GetByUser(int userId);

    int CreateBudget(Budget budget);

    void InsertExpenses(
        int budgetId,
        IReadOnlyList<Expense> expenses);

    void InsertSavings(
        int budgetId,
        IReadOnlyList<Saving> savings);

    void InsertCategories(
        int budgetId,
        Dictionary<string, decimal> categories);

    Dictionary<string, decimal>
        GetCategories(int budgetId);

    void DeleteByBudgetId(int budgetId);

    void DeleteByUserId(int userId);
}