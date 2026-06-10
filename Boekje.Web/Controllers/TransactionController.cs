using Boekje.Domain.Services;
using Boekje.Web.ViewModels.Transaction;
using Microsoft.AspNetCore.Mvc;

namespace Boekje.Web.Controllers;

public class TransactionController : Controller
{
    private readonly TransactionService
        _service;

    public TransactionController(
        TransactionService service)
    {
        _service = service;
    }

    [HttpPost]
    public IActionResult Add(
        AddTransactionViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return RedirectToAction(
                "Index",
                "Dashboard");
        }

        var userId =
            HttpContext.Session
                .GetInt32(
                    "UserId");

        if (userId == null)
        {
            return RedirectToAction(
                "Login",
                "Auth");
        }

        _service.AddExpense(
            userId.Value,
            model.Amount,
            model.Category,
            model.Description);

        return RedirectToAction(
            "Index",
            "Dashboard");
    }
}