using Boekje.Domain.Services;
using Boekje.Tests.Fakes;

namespace Boekje.Tests.Services;

public class TransactionServiceTests
{
    [Fact]
    public void AddExpense_AddsTransaction()
    {
        var repo =
            new FakeTransactionRepository();

        var service =
            new TransactionService(repo);

        service.AddExpense(
            1,
            100,
            "Food",
            "Pizza");

        Assert.Single(
            repo.Transactions);
    }

    [Fact]
    public void Balance_IsCalculatedCorrectly()
    {
        var repo =
            new FakeTransactionRepository();

        var service =
            new TransactionService(repo);

        service.AddIncome(
            1,
            1000,
            "Salary",
            "Salary");

        service.AddExpense(
            1,
            250,
            "Food",
            "Pizza");

        var balance =
            service.GetBalance(1);

        Assert.Equal(
            750,
            balance);
    }
}