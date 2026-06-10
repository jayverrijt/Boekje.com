using Boekje.Domain.Services;
using Boekje.Web.ViewModels.Dashboard;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Boekje.Web.Controllers;

public class DashboardController : Controller
{
    private readonly DashboardService
        _dashboardService;

    public DashboardController(
        DashboardService dashboardService)
    {
        _dashboardService =
            dashboardService;
    }

    public async Task<IActionResult> Index()
    {
        var email =
            HttpContext.Session
                .GetString(
                    "UserEmail");

        if (email == null)
        {
            return RedirectToAction(
                "Login",
                "Auth");
        }

        var data =
            await _dashboardService
                .GetDashboardAsync(
                    email);

        var vm =
            new DashboardViewModel
            {
                TotalIncome =
                    data.Income,

                TotalExpenses =
                    data.Expenses,

                Remaining =
                    data.Total
            };

        return View(vm);
    }
}