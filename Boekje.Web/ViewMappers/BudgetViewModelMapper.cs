using Boekje.Domain.Entities;
using Boekje.Web.ViewModels.Budget;

namespace Boekje.Web.ViewMappers;

public static class BudgetViewModelMapper
{
    public static BudgetViewModel
        ToViewModel(
            Budget budget)
    {
        return new BudgetViewModel
        {
            Income =
                budget.Income,

            Expenses =
                budget.Expenses
                    .Select(x =>
                        new ExpenseViewModel
                        {
                            Name = x.Name,
                            Type = x.Type,
                            Amount = x.Amount
                        })
                    .ToList(),

            Savings =
                budget.Savings
                    .Select(x =>
                        new SavingViewModel
                        {
                            Name = x.Name,
                            Amount = x.Amount
                        })
                    .ToList(),

            TotalExpenses =
                budget.GetTotalExpenses(),

            TotalSavings =
                budget.GetTotalSavings(),

            Remaining =
                budget.GetRemaining(),

            IsOverBudget =
                budget.IsOverBudget()
        };
    }

    public static BudgetEditViewModel
        ToEditViewModel(
            Budget budget)
    {
        return new BudgetEditViewModel
        {
            Income =
                budget.Income,

            Expenses =
                budget.Expenses
                    .Select(x =>
                        new ExpenseViewModel
                        {
                            Name = x.Name,
                            Type = x.Type,
                            Amount = x.Amount
                        })
                    .ToList(),

            Savings =
                budget.Savings
                    .Select(x =>
                        new SavingViewModel
                        {
                            Name = x.Name,
                            Amount = x.Amount
                        })
                    .ToList()
        };
    }
}