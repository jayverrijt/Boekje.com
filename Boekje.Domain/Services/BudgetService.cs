using Boekje.Domain.Entities;
using Boekje.Domain.Interfaces;

namespace Boekje.Domain.Services;

public class BudgetService
{
    private readonly IBudgetRepository _repo;

    public BudgetService(IBudgetRepository repo)
    {
        _repo = repo;
    }

    public (Budget?, Dictionary<string, decimal>) GetFullBudget(int userId)
    {
        var budget = _repo.GetByUser(userId);

        var categories = new Dictionary<string, decimal>();

        if (budget != null)
        {
            categories = _repo.GetCategories(budget.Id);
        }

        return (budget, categories);
    }

    public void Save(Budget budget, Dictionary<string, decimal> categories)
    {
        var existing = _repo.GetByUser(budget.UserId);

        int budgetId;

        if (existing == null)
        {
            budgetId = _repo.CreateBudget(budget);
        }
        else
        {
            budgetId = existing.Id;
            _repo.DeleteByBudgetId(budgetId);
        }

        _repo.InsertExpenses(budgetId, budget.Expenses);
        _repo.InsertSavings(budgetId, budget.Savings);
        _repo.InsertCategories(budgetId, categories);
    }

    public bool HasBudget(int userId)
    {
        return _repo.GetByUser(userId) != null;
    }

    public void Delete(int userId)
    {
        _repo.DeleteByUserId(userId);
    }
}