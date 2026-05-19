using System.ComponentModel.DataAnnotations;

namespace Boekje.Web.ViewModels.Transaction;

public class AddTransactionViewModel
{
    [Required]
    [Range(0.01, 999999)]
    public decimal Amount
    {
        get;
        set;
    }

    [Required]
    public string Type
    {
        get;
        set;
    } = string.Empty;

    [Required]
    public string Category
    {
        get;
        set;
    } = string.Empty;

    public string? Description
    {
        get;
        set;
    }
}