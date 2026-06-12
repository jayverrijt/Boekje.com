using Boekje.Domain.Entities;
using Boekje.Domain.Interfaces;

namespace Boekje.Tests.Fakes;

public class FakeTransactionRepository
    : ITransactionRepository
{
    public List<Transaction>
        Transactions = new();

    public void AddTransaction(
        Transaction transaction)
    {
        Transactions.Add(
            transaction);
    }

    public List<Transaction>
        GetByUser(
            int userId)
    {
        return Transactions
            .Where(x =>
                x.UserId == userId)
            .ToList();
    }
}