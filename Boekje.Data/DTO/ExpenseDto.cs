namespace Boekje.Data.DTO;

public class ExpenseDto
{
    public int BudgetId { get; set; }

    public string Type { get; set; }
        = string.Empty;

    public decimal Amount { get; set; }

    public string Name { get; set; }
        = string.Empty;
}