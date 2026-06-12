using Boekje.Domain.Entities;

namespace Boekje.Tests.Entities;

public class TransactionTests
{
    [Fact]
    public void CreateExpense_CreatesExpense()
    {
        var tx =
            Transaction.CreateExpense(
                1,
                100,
                "Food",
                "Pizza");

        Assert.True(
            tx.IsExpense());
    }

    [Fact]
    public void CreateIncome_CreatesIncome()
    {
        var tx =
            Transaction.CreateIncome(
                1,
                1000,
                "Salary",
                null);

        Assert.True(
            tx.IsIncome());
    }

    [Fact]
    public void NegativeAmount_Throws()
    {
        Assert.Throws<Exception>(
            () =>
                new Transaction(
                    1,
                    -1,
                    "income",
                    "salary",
                    null));
    }
}