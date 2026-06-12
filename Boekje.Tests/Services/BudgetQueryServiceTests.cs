using Boekje.Domain.Services;
using Boekje.Tests.Fakes;
using Xunit;

namespace Boekje.Tests.Services;

public class BudgetQueryServiceTests
{
    [Fact]
    public void HasBudget_ReturnsFalse_WhenBudgetMissing()
    {
        var repo =
            new FakeBudgetRepository();

        var service =
            new BudgetQueryService(repo);

        var result =
            service.HasBudget(1);

        Assert.False(result);
    }
}