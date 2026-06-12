using Boekje.Domain.Services;

namespace Boekje.Tests.Services;

public class DashboardServiceTests
{
    [Fact]
    public async Task GetDashboard_ReturnsData()
    {
        var service =
            new DashboardService();

        var result =
            await service.GetDashboardAsync(
                "test@test.com");

        Assert.NotNull(
            result);

        Assert.Equal(
            1135,
            result.Income);

        Assert.Equal(
            835,
            result.Expenses);

        Assert.Equal(
            300,
            result.Savings);

        Assert.Equal(
            1200,
            result.Total);
    }
}