using System.ComponentModel.DataAnnotations;

namespace Boekje.Web.ViewModels.Budget;

public class BudgetViewModel
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

    public decimal TotalExpenses
    {
        get;
        set;
    }

    public decimal TotalSavings
    {
        get;
        set;
    }

    public decimal Remaining
    {
        get;
        set;
    }

    public bool IsOverBudget
    {
        get;
        set;
    }
}