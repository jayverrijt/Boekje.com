using Boekje.Domain.Services;
using Boekje.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Boekje.Web.Controllers;

public class AuthController : Controller
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    public IActionResult Login() => View();

    [HttpPost]
    public IActionResult Login(LoginViewModel vm)
    {
        var user = _authService.Login(vm.Email, vm.Password);

        if (user == null)
        {
            ModelState.AddModelError("", "Invalid login");
            return View(vm);
        }

        HttpContext.Session.SetString("UserEmail", user.Email);

        return RedirectToAction("Index", "Home");
    }

    public IActionResult Register() => View();

    [HttpPost]
    public IActionResult Register(RegisterViewModel vm)
    {
        var success = _authService.Register(vm.Email, vm.Password);

        if (!success)
        {
            ModelState.AddModelError("", "User already exists");
            return View(vm);
        }

        return RedirectToAction("Login");
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Login");
    }
}