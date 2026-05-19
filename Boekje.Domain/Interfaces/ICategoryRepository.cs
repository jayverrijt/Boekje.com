namespace Boekje.Domain.Interfaces;

public interface ICategoryRepository
{
    Dictionary<string, decimal>
        GetByBudget(int budgetId);

    void Insert(
        int budgetId,
        string name,
        decimal amount);

    void DeleteByBudget(
        int budgetId);
}