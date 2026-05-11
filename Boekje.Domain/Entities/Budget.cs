namespace Boekje.Domain.Entities;

public class Budget
{
    public int Id { get; set; }
    public int UserId { get; set; }

    public decimal Income { get; set; }

    // simpel voor nu
    public List<Expense> Expenses { get; set; } = new();
    public List<Saving> Savings { get; set; } = new();
}