namespace Boekje.Domain.Entities;

public class Budget
{
    private readonly List<Expense>
        _expenses = new();

    private readonly List<Saving>
        _savings = new();

    public int Id { get; private set; }

    public int UserId { get; private set; }

    public decimal Income { get; private set; }

    public IReadOnlyList<Expense>
        Expenses => _expenses;

    public IReadOnlyList<Saving>
        Savings => _savings;

    public Budget(
        int userId,
        decimal income)
    {
        if (income < 0)
        {
            throw new Exception(
                "Income cannot be negative.");
        }

        UserId = userId;

        Income = income;
    }

    public void SetId(int id)
    {
        Id = id;
    }


    public void UpdateIncome(
        decimal amount)
    {
        if (amount < 0)
        {
            throw new Exception(
                "Income cannot be negative.");
        }

        Income = amount;
    }

    public void AddExpense(
        Expense expense)
    {
        if (expense == null)
        {
            throw new Exception(
                "Expense is required.");
        }

        if (_expenses.Any(x =>
                x.Name == expense.Name))
        {
            throw new Exception(
                "Expense already exists.");
        }

        _expenses.Add(expense);
    }

    public void AddSaving(
        Saving saving)
    {
        if (saving == null)
        {
            throw new Exception(
                "Saving is required.");
        }

        if (_savings.Any(x =>
                x.Name == saving.Name))
        {
            throw new Exception(
                "Saving already exists.");
        }

        _savings.Add(saving);
    }


    public decimal GetTotalExpenses()
    {
        return _expenses.Sum(
            x => x.Amount);
    }

    public decimal GetTotalSavings()
    {
        return _savings.Sum(
            x => x.Amount);
    }

    public decimal GetRemaining()
    {
        return Income
             - GetTotalExpenses()
             - GetTotalSavings();
    }

    public bool IsOverBudget()
    {
        return GetRemaining() < 0;
    }

    public bool HasExpenses()
    {
        return _expenses.Any();
    }

    public bool HasSavings()
    {
        return _savings.Any();
    }


}