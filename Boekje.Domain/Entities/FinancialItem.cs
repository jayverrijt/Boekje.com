namespace Boekje.Domain.Entities;

public abstract class FinancialItem
{
    public decimal Amount { get; protected set; }

    public string Name { get; protected set; }

    protected FinancialItem(
        decimal amount,
        string name)
    {
        if (amount <= 0)
        {
            throw new Exception(
                "Amount must be positive.");
        }

        if (string.IsNullOrWhiteSpace(name))
        {
            throw new Exception(
                "Name is required.");
        }

        Amount = amount;

        Name = name;
    }

    public void UpdateAmount(
        decimal amount)
    {
        if (amount <= 0)
        {
            throw new Exception(
                "Amount must be positive.");
        }

        Amount = amount;
    }

    public void Rename(
        string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new Exception(
                "Name is required.");
        }

        Name = name;
    }
}