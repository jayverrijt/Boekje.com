using Boekje.Domain.Entities;

namespace Boekje.Domain.Interfaces;

public interface ISavingRepository
{
    List<Saving> GetByBudget(
        int budgetId);

    void Insert(
        int budgetId,
        Saving saving);

    void DeleteByBudget(
        int budgetId);
}