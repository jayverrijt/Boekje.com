using System.ComponentModel.DataAnnotations;

namespace Boekje.Web.ViewModels;

public class LoginViewModel
{
    [Required(
        ErrorMessage =
            "Email is verplicht")]
    [EmailAddress(
        ErrorMessage =
            "Voer een geldig emailadres in")]
    public string Email { get; set; }
        = "";

    [Required(
        ErrorMessage =
            "Wachtwoord is verplicht")]
    public string Password { get; set; }
        = "";
}