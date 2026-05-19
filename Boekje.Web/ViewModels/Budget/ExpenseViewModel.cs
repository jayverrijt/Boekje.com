using System.ComponentModel.DataAnnotations;

namespace Boekje.Web.ViewModels.Budget;

public class ExpenseViewModel
{
    [Required]
    public string Name { get; set; }
        = string.Empty;

    [Required]
    public string Type { get; set; }
        = string.Empty;

    [Range(0.01, 999999)]
    public decimal Amount { get; set; }
}