using Boekje.Domain.Entities;
using Boekje.Domain.Interfaces;

namespace Boekje.Domain.Services;

public class BudgetCommandService
{
    private readonly IBudgetRepository
        _budgetRepository;

    private readonly IExpenseRepository
        _expenseRepository;

    private readonly ISavingRepository
        _savingRepository;

    public BudgetCommandService(
        IBudgetRepository budgetRepository,
        IExpenseRepository expenseRepository,
        ISavingRepository savingRepository)
    {
        _budgetRepository =
            budgetRepository;

        _expenseRepository =
            expenseRepository;

        _savingRepository =
            savingRepository;
    }

    public void Save(
        Budget budget)
    {
        var existing =
            _budgetRepository
                .GetByUser(
                    budget.UserId);

        if (existing != null)
        {
            Delete(
                budget.UserId);
        }

        var budgetId =
            _budgetRepository
                .CreateBudget(
                    budget);

        foreach (var expense
                 in budget.Expenses)
        {
            _expenseRepository
                .Insert(
                    budgetId,
                    expense);
        }

        foreach (var saving
                 in budget.Savings)
        {
            _savingRepository
                .Insert(
                    budgetId,
                    saving);
        }
    }

    public void Delete(
        int userId)
    {
        var budget =
            _budgetRepository
                .GetByUser(
                    userId);

        if (budget == null)
        {
            return;
        }

        _expenseRepository
            .DeleteByBudget(
                budget.Id);

        _savingRepository
            .DeleteByBudget(
                budget.Id);

        _budgetRepository
            .DeleteByBudgetId(
                budget.Id);
    }
}