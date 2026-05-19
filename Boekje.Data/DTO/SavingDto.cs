namespace Boekje.Data.DTO;

public class SavingDto
{
    public int BudgetId { get; set; }

    public decimal Amount { get; set; }

    public string Name { get; set; }
        = string.Empty;
}