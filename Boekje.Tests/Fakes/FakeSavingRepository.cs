using Boekje.Domain.Entities;
using Boekje.Domain.Interfaces;

namespace Boekje.Tests.Fakes;

public class FakeSavingRepository
    : ISavingRepository
{
    public List<Saving> Savings
        = new();

    public List<Saving> GetByBudget(
        int budgetId)
    {
        return Savings;
    }

    public void Insert(
        int budgetId,
        Saving saving)
    {
        Savings.Add(saving);
    }

    public void DeleteByBudget(
        int budgetId)
    {
        Savings.Clear();
    }
}