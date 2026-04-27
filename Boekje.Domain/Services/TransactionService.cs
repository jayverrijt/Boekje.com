using Boekje.Domain.Entities;
using Boekje.Domain.Interfaces;

namespace Boekje.Domain.Services;

public class TransactionService
{
    private readonly ITransactionRepository _repo;

    public TransactionService(ITransactionRepository repo)
    {
        _repo = repo;
    }

    public void AddExpense(int userId, decimal amount, string category, string description)
    {
        if (amount <= 0)
            throw new Exception("Amount must be positive");

        var transaction = new Transaction
        {
            UserId = userId,
            Amount = amount,
            Type = "expense",
            Category = category,
            Description = description,
            CreatedAt = DateTime.Now
        };

        _repo.AddTransaction(transaction);
    }

    public List<Transaction> GetUserTransactions(int userId)
    {
        return _repo.GetByUser(userId);
    }
}