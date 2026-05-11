using Microsoft.AspNetCore.Mvc;
using Boekje.Domain.Services;
using Boekje.Domain.Entities;

public class BudgetController : Controller
{
    private readonly BudgetService _budgetService;

    public BudgetController(BudgetService budgetService)
    {
        _budgetService = budgetService;
    }

    // =========================
    // INDEX
    // =========================
    public IActionResult Index()
    {
        int userId = GetUserId();

        if (!_budgetService.HasBudget(userId))
            return RedirectToAction("Add");

        var (budget, categories) = _budgetService.GetFullBudget(userId);

        ViewBag.Categories = categories;

        return View(budget);
    }

    // =========================
    // ADD
    // =========================
    public IActionResult Add()
    {
        return View();
    }

    // =========================
    // EDIT
    // =========================
    public IActionResult Edit()
    {
        int userId = GetUserId();

        var (budget, categories) = _budgetService.GetFullBudget(userId);

        if (budget == null)
            return RedirectToAction("Add");

        ViewBag.Categories = categories;

        return View(budget);
    }

    // =========================
    // SAVE (POST)
    // =========================
    [HttpPost]
    public IActionResult Save(Budget model)
    {
        int userId = GetUserId();
        model.UserId = userId;

        var categories = new Dictionary<string, decimal>
        {
            { "Boodschappen", ParseDecimal(Request.Form["Boodschappen"]) },
            { "OV", ParseDecimal(Request.Form["OV"]) },
            { "Benzine", ParseDecimal(Request.Form["Benzine"]) },
            { "Uitgaan", ParseDecimal(Request.Form["Uitgaan"]) },
            { "Eten", ParseDecimal(Request.Form["Eten"]) },
            { "Games", ParseDecimal(Request.Form["Games"]) },
            { "Kleding", ParseDecimal(Request.Form["Kleding"]) },
            { "Gezondheid", ParseDecimal(Request.Form["Gezondheid"]) }
        };

        _budgetService.Save(model, categories);

        return RedirectToAction("Index");
    }

    // =========================
    // HELPERS
    // =========================

    private int GetUserId()
    {
        var userId = HttpContext.Session.GetString("UserId");

        if (userId == null)
            return 1;

        return int.Parse(userId);
    }

    private decimal ParseDecimal(string value)
    {
        return string.IsNullOrWhiteSpace(value) ? 0 : decimal.Parse(value);
    }

    [HttpPost]
    public IActionResult Delete()
    {
        int userId = GetUserId();

        _budgetService.Delete(userId);

        return RedirectToAction("Add");
    }
}