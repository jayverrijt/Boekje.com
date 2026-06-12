using Boekje.Domain.Interfaces;

namespace Boekje.Web.ViewModels;

public class BudgetRuleSettings
    : IBudgetRuleSettings
{
    public decimal MaxRentPercentage
    {
        get;
        set;
    }

    public decimal MinSavingsPercentage
    {
        get;
        set;
    }
}