namespace Boekje.Domain.Entities;

public class Expense
{
    public string Type { get; set; } = "";
    public decimal Amount { get; set; }
    public string Name { get; set; } = "";
}