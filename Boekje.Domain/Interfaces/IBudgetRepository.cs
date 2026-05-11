using Boekje.Domain.Entities;

namespace Boekje.Domain.Interfaces;

public interface IBudgetRepository
{
    Budget? GetByUser(int userId);

    int CreateBudget(Budget budget);

    void DeleteByBudgetId(int budgetId);

    void InsertExpenses(int budgetId, List<Expense> expenses);

    void InsertSavings(int budgetId, List<Saving> savings);

    void InsertCategories(int budgetId, Dictionary<string, decimal> categories);
    void DeleteByUserId(int userId);
    Dictionary<string, decimal> GetCategories(int budgetId);

}