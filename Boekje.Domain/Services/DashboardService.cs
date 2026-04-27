namespace Boekje.Domain.Services;

public class DashboardService
{
    public Task<DashboardData> GetDashboardAsync(string userEmail)
    {
        // Dummy data (later vervangen door repositories)
        var data = new DashboardData
        {
            Income = 1135,
            Expenses = 835,
            Savings = 300,
            Total = 1200,
            Month = "Mei 2026"
        };

        return Task.FromResult(data);
    }
}

public class DashboardData
{
    public decimal Income { get; set; }
    public decimal Expenses { get; set; }
    public decimal Savings { get; set; }
    public decimal Total { get; set; }
    public string Month { get; set; }
}