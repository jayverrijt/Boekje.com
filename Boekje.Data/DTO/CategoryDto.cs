namespace Boekje.Data.DTO;

public class CategoryDto
{
    public int BudgetId { get; set; }

    public string Name { get; set; }
        = string.Empty;

    public decimal Amount { get; set; }
}