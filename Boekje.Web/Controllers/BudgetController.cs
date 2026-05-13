using Boekje.Domain.Entities;
using Boekje.Domain.Services;
using Boekje.Web.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Boekje.Web.Controllers;

public class BudgetController : Controller
{
    private readonly BudgetService _budgetService;
    private readonly CurrentUserService _currentUser;

    public BudgetController(
        BudgetService budgetService,
        CurrentUserService currentUser)
    {
        _budgetService = budgetService;
        _currentUser = currentUser;
    }

    /* =========================
       INDEX
    ========================= */

    public IActionResult Index()
    {
        return View();
    }

    /* =========================
       ADD
    ========================= */

    [HttpGet]
    public IActionResult Add()
    {
        return View(new Budget());
    }

    /* =========================
       EDIT
    ========================= */

    [HttpGet]
    public IActionResult Edit()
    {
        return View();
    }

    /* =========================
       SAVE
    ========================= */

    [HttpPost]
    public IActionResult Save(Budget model)
    {
        var categories =
            new Dictionary<string, decimal>();

        foreach (var key in Request.Form.Keys)
        {
            if (decimal.TryParse(
                    Request.Form[key],
                    out var value))
            {
                categories[key] = value;
            }
        }

        _budgetService.Save(
            model,
            categories);

        return RedirectToAction("Index");
    }

    /* =========================
       DELETE
    ========================= */

    [HttpPost]
    public IActionResult Delete()
    {
        return RedirectToAction("Index");
    }
}