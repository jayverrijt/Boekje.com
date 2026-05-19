using Boekje.Domain.Entities;

namespace Boekje.Domain.Interfaces;

public interface ITransactionRepository
{
    void AddTransaction(
        Transaction transaction);

    List<Transaction> GetByUser(
        int userId);
}