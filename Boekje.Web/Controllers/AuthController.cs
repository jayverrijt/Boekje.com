using Boekje.Domain.Services;
using Boekje.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

public class AuthController : Controller
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpGet]
    public IActionResult Login() => View();

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var user = await _authService.LoginAsync(model.Email, model.Password);

        if (user == null)
        {
            ModelState.AddModelError("", "Email of wachtwoord is incorrect");
            return View(model);
        }

        HttpContext.Session.SetString("UserEmail", user.Email);

        return RedirectToAction("Index", "Dashboard");
    }

    [HttpGet]
    public IActionResult Register() => View();

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var success = await _authService.RegisterAsync(model.Email, model.Password);

        if (!success)
        {
            ModelState.AddModelError("", "Email bestaat al");
            return View(model);
        }

        return RedirectToAction("Login");
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Login");
    }
}