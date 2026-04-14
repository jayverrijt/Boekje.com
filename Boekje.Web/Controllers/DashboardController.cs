using Boekje.Domain.Services;
using Boekje.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

public class DashboardController : Controller
{
    private readonly DashboardService _dashboardService;

    public DashboardController(DashboardService dashboardService)
    {
        _dashboardService = dashboardService;
    }

    public async Task<IActionResult> Index()
    {
        var email = HttpContext.Session.GetString("UserEmail");

        if (email == null)
            return RedirectToAction("Login", "Auth");

        var data = await _dashboardService.GetDashboardAsync(email);

        var vm = new DashboardViewModel
        {
            Income = data.Income,
            Expenses = data.Expenses,
            Savings = data.Savings,
            Total = data.Total,
            Month = data.Month
        };

        return View(vm);
    }
}