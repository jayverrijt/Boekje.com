namespace Boekje.Domain.Entities;

public class Transaction
{
    public int Id { get; private set; }

    public int UserId { get; private set; }

    public decimal Amount { get; private set; }

    public string Type { get; private set; }

    public string Category { get; private set; }

    public string? Description { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public Transaction(
        int userId,
        decimal amount,
        string type,
        string category,
        string? description)
    {
        if (amount <= 0)
        {
            throw new Exception(
                "Transaction amount must be positive.");
        }

        if (string.IsNullOrWhiteSpace(type))
        {
            throw new Exception(
                "Transaction type is required.");
        }

        if (string.IsNullOrWhiteSpace(category))
        {
            throw new Exception(
                "Transaction category is required.");
        }

        UserId = userId;

        Amount = amount;

        Type = type;

        Category = category;

        Description = description;

        CreatedAt = DateTime.Now;
    }

    /*
     * Persistence helpers
     */

    public void SetId(int id)
    {
        Id = id;
    }

    public void SetCreatedAt(
        DateTime createdAt)
    {
        CreatedAt = createdAt;
    }

    /*
     * Domain behavior
     */

    public bool IsExpense()
    {
        return Type == "expense";
    }

    public bool IsIncome()
    {
        return Type == "income";
    }

    /*
     * Named constructors
     */

    public static Transaction CreateExpense(
        int userId,
        decimal amount,
        string category,
        string? description)
    {
        return new Transaction(
            userId,
            amount,
            "expense",
            category,
            description);
    }

    public static Transaction CreateIncome(
        int userId,
        decimal amount,
        string category,
        string? description)
    {
        return new Transaction(
            userId,
            amount,
            "income",
            category,
            description);
    }
}