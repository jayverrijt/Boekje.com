namespace Boekje.Domain.Entities;

public class Expense : FinancialItem
{
    public string Type { get; private set; }

    public Expense(
        string type,
        decimal amount,
        string name)
        : base(amount, name)
    {
        if (string.IsNullOrWhiteSpace(type))
        {
            throw new Exception(
                "Expense type is required.");
        }

        Type = type;
    }

    public void ChangeType(
        string type)
    {
        if (string.IsNullOrWhiteSpace(type))
        {
            throw new Exception(
                "Expense type is required.");
        }

        Type = type;
    }
}