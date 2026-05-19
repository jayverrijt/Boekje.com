namespace Boekje.Web.ViewModels.Budget;

public class BudgetAdviceViewModel
{
    public decimal
        RecommendedMaxRent
    {
        get;
        set;
    }

    public decimal
        RecommendedMinimumSavings
    {
        get;
        set;
    }

    public List<string>
        Warnings
    {
        get;
        set;
    } = new();
}