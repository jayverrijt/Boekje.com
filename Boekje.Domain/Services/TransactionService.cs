using Boekje.Domain.Entities;
using Boekje.Domain.Interfaces;

namespace Boekje.Domain.Services;

public class TransactionService
{
    private readonly ITransactionRepository _repo;

    public TransactionService(
        ITransactionRepository repo)
    {
        _repo = repo;
    }

    public void AddExpense(
        int userId,
        decimal amount,
        string category,
        string description)
    {
        var transaction =
            Transaction.CreateExpense(
                userId,
                amount,
                category,
                description);

        _repo.AddTransaction(transaction);
    }

    public void AddIncome(
        int userId,
        decimal amount,
        string category,
        string description)
    {
        var transaction =
            Transaction.CreateIncome(
                userId,
                amount,
                category,
                description);

        _repo.AddTransaction(transaction);
    }

    public List<Transaction>
        GetUserTransactions(int userId)
    {
        return _repo.GetByUser(userId);
    }

    public decimal GetTotalExpenses(
        int userId)
    {
        return _repo
            .GetByUser(userId)
            .Where(x => x.IsExpense())
            .Sum(x => x.Amount);
    }

    public decimal GetTotalIncome(
        int userId)
    {
        return _repo
            .GetByUser(userId)
            .Where(x => x.IsIncome())
            .Sum(x => x.Amount);
    }

    public decimal GetBalance(
        int userId)
    {
        return GetTotalIncome(userId)
               - GetTotalExpenses(userId);
    }
}