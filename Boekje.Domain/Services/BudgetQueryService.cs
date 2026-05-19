using Boekje.Domain.Entities;
using Boekje.Domain.Interfaces;

namespace Boekje.Domain.Services;

public class BudgetQueryService
{
    private readonly IBudgetRepository
        _budgetRepository;

    public BudgetQueryService(
        IBudgetRepository budgetRepository)
    {
        _budgetRepository =
            budgetRepository;
    }

    public Budget? GetByUser(
        int userId)
    {
        return _budgetRepository
            .GetByUser(userId);
    }

    public bool HasBudget(
        int userId)
    {
        return _budgetRepository
                   .GetByUser(userId)
               != null;
    }
}