using Boekje.Domain.Entities;
using Boekje.Domain.Services;
using Boekje.Web.Infrastructure;
using Boekje.Web.ViewMappers;
using Boekje.Web.ViewModels.Budget;
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

        var budget =
            _budgetService.GetFullBudget(
                _currentUser.UserId);

        if (budget == null)
        {
            return RedirectToAction(
                "Add");
        }

        var advice =
            _budgetService
                .GenerateAdvice(
                    budget);

        ViewBag.Advice =
            advice;

        ViewBag.Categories =
            new Dictionary<string, decimal>();

        var model =
            BudgetViewModelMapper
                .ToViewModel(
                    budget);

        return View(model);
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

        return View(
            new BudgetEditViewModel());
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

        var model =
            BudgetViewModelMapper
                .ToEditViewModel(
                    budget);

        return View(model);
    }

    /* =========================
       SAVE
    ========================= */

    [HttpPost]
    public IActionResult Save(
        BudgetEditViewModel model)
    {
        if (!_currentUser.IsAuthenticated)
        {
            return RedirectToAction(
                "Login",
                "Auth");
        }

        if (!ModelState.IsValid)
        {
            return View(
                "Edit",
                model);
        }

        var budget =
            new Budget(
                _currentUser.UserId,
                model.Income);

        foreach (var expenseModel
                 in model.Expenses)
        {
            var expense =
                new Expense(
                    expenseModel.Type,
                    expenseModel.Amount,
                    expenseModel.Name);

            budget.AddExpense(
                expense);
        }

        foreach (var savingModel
                 in model.Savings)
        {
            var saving =
                new Saving(
                    savingModel.Amount,
                    savingModel.Name);

            budget.AddSaving(
                saving);
        }

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