using Boekje.Web.ViewModels.Transaction;

namespace Boekje.Web.ViewModels.Dashboard;

public class DashboardViewModel
{
    public decimal TotalIncome
    {
        get;
        set;
    }

    public decimal TotalExpenses
    {
        get;
        set;
    }

    public decimal Remaining
    {
        get;
        set;
    }

    public List<TransactionViewModel>
        RecentTransactions
    {
        get;
        set;
    } = new();
}