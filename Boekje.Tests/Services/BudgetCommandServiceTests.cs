using Boekje.Domain.Entities;
using Boekje.Domain.Services;
using Boekje.Tests.Fakes;

namespace Boekje.Tests.Services;

public class BudgetCommandServiceTests
{
    [Fact]
    public void Save_CreatesBudget()
    {
        var budgetRepo =
            new FakeBudgetRepository();

        var expenseRepo =
            new FakeExpenseRepository();

        var savingRepo =
            new FakeSavingRepository();

        var service =
            new BudgetCommandService(
                budgetRepo,
                expenseRepo,
                savingRepo);

        var budget =
            new Budget(
                1,
                2000);

        service.Save(
            budget);

        Assert.Single(
            budgetRepo.Budgets);
    }

    [Fact]
    public void Delete_RemovesBudget()
    {
        var budgetRepo =
            new FakeBudgetRepository();

        var expenseRepo =
            new FakeExpenseRepository();

        var savingRepo =
            new FakeSavingRepository();

        var budget =
            new Budget(
                1,
                2000);

        budgetRepo.CreateBudget(
            budget);

        var service =
            new BudgetCommandService(
                budgetRepo,
                expenseRepo,
                savingRepo);

        service.Delete(
            1);

        Assert.Empty(
            budgetRepo.Budgets);
    }
}