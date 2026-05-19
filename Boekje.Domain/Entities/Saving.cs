namespace Boekje.Domain.Entities;

public class Saving : FinancialItem
{
    public Saving(
        decimal amount,
        string name)
        : base(amount, name)
    {
    }
}