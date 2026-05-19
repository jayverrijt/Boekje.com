using Boekje.Domain.Services;
using Boekje.Web.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Boekje.Web.Controllers;

public class AuthController : Controller
{
    private readonly AuthService
        _authService;

    public AuthController(
        AuthService authService)
    {
        _authService =
            authService;
    }

    /* =========================
       LOGIN
    ========================= */

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(
        LoginViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var user =
            await _authService
                .LoginAsync(
                    model.Email,
                    model.Password);

        if (user == null)
        {
            ModelState.AddModelError(
                "",
                "Email of wachtwoord is incorrect");

            return View(model);
        }

        /*
         * Save session
         */

        HttpContext.Session.SetString(
            "UserEmail",
            user.Email);

        HttpContext.Session.SetInt32(
            "UserId",
            user.Id);

        return RedirectToAction(
            "Index",
            "Dashboard");
    }

    /* =========================
       REGISTER
    ========================= */

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(
        RegisterViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        /*
         * Register rich domain user
         */

        var success =
            await _authService
                .RegisterAsync(
                    model.Name,
                    model.Email,
                    model.Password);

        if (!success)
        {
            ModelState.AddModelError(
                "",
                "Email bestaat al");

            return View(model);
        }

        return RedirectToAction(
            "Login");
    }

    /* =========================
       LOGOUT
    ========================= */

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();

        return RedirectToAction(
            "Login");
    }
}