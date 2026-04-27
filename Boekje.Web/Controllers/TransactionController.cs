using Microsoft.AspNetCore.Mvc;
using Boekje.Domain.Services;

public class TransactionController : Controller
{
    private readonly TransactionService _service;

    public TransactionController(TransactionService service)
    {
        _service = service;
    }

    [HttpPost]
    public IActionResult Add(decimal amount, string category, string description)
    {
        int userId = 1; // tijdelijk hardcoded

        _service.AddExpense(userId, amount, category, description);

        return RedirectToAction("Index", "Home");
    }
}