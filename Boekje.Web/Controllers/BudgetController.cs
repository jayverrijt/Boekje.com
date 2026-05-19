using Boekje.Domain.Entities;
using Boekje.Domain.Services;
using Boekje.Web.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Boekje.Web.Controllers;

public class BudgetController : Controller
{
    private readonly BudgetService
        _budgetService;

    private readonly CurrentUserService
        _currentUser;

    public BudgetController(
        BudgetService budgetService,
        CurrentUserService currentUser)
    {
        _budgetService =
            budgetService;

        _currentUser =
            currentUser;
    }

    /* =========================
       INDEX
    ========================= */

    public IActionResult Index()
    {
        if (!_currentUser.IsAuthenticated)
        {
            return RedirectToAction(
                "Login",
                "Auth");
        }

        /*
         * Get user budget
         */

        var budget =
            _budgetService.GetFullBudget(
                _currentUser.UserId);

        /*
         * No budget yet
         */

        if (budget == null)
        {
            return RedirectToAction(
                "Add");
        }

        /*
         * Generate advice
         */

        var advice =
            _budgetService
                .GenerateAdvice(
                    budget);

        ViewBag.Advice =
            advice;

        /*
         * Temporary empty categories
         */

        ViewBag.Categories =
            new Dictionary<string, decimal>();

        return View(budget);
    }

    /* =========================
       ADD
    ========================= */

    [HttpGet]
    public IActionResult Add()
    {
        if (!_currentUser.IsAuthenticated)
        {
            return RedirectToAction(
                "Login",
                "Auth");
        }

        var budget =
            new Budget(
                _currentUser.UserId,
                0);

        return View(budget);
    }

    /* =========================
       EDIT
    ========================= */

    [HttpGet]
    public IActionResult Edit()
    {
        if (!_currentUser.IsAuthenticated)
        {
            return RedirectToAction(
                "Login",
                "Auth");
        }

        var budget =
            _budgetService.GetFullBudget(
                _currentUser.UserId);

        if (budget == null)
        {
            return RedirectToAction(
                "Add");
        }

        ViewBag.Categories =
            new Dictionary<string, decimal>();

        return View(budget);
    }

    /* =========================
       SAVE
    ========================= */

    [HttpPost]
    public IActionResult Save(
        decimal income)
    {
        if (!_currentUser.IsAuthenticated)
        {
            return RedirectToAction(
                "Login",
                "Auth");
        }

        /*
         * Create rich budget model
         */

        var budget =
            new Budget(
                _currentUser.UserId,
                income);

        /*
         * Expenses
         */

        foreach (var key in Request.Form.Keys)
        {
            if (key.StartsWith(
                    "expense_"))
            {
                if (decimal.TryParse(
                        Request.Form[key],
                        out var amount))
                {
                    var expense =
                        new Expense(
                            "General",
                            amount,
                            key);

                    budget.AddExpense(
                        expense);
                }
            }
        }

        /*
         * Savings
         */

        foreach (var key in Request.Form.Keys)
        {
            if (key.StartsWith(
                    "saving_"))
            {
                if (decimal.TryParse(
                        Request.Form[key],
                        out var amount))
                {
                    var saving =
                        new Saving(
                            amount,
                            key);

                    budget.AddSaving(
                        saving);
                }
            }
        }

        /*
         * Save budget
         */

        _budgetService.Save(
            budget);

        return RedirectToAction(
            "Index");
    }

    /* =========================
       DELETE
    ========================= */

    [HttpPost]
    public IActionResult Delete()
    {
        if (!_currentUser.IsAuthenticated)
        {
            return RedirectToAction(
                "Login",
                "Auth");
        }

        _budgetService.Delete(
            _currentUser.UserId);

        return RedirectToAction(
            "Index");
    }
}