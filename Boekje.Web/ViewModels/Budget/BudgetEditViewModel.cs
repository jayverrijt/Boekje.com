using System.ComponentModel.DataAnnotations;

namespace Boekje.Web.ViewModels.Budget;

public class BudgetEditViewModel
{
    [Required]
    [Range(0, 999999)]
    public decimal Income { get; set; }

    public List<ExpenseViewModel>
        Expenses { get; set; }
        = new();

    public List<SavingViewModel>
        Savings { get; set; }
        = new();
}