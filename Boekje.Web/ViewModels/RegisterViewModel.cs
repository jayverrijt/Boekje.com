using System.ComponentModel.DataAnnotations;

namespace Boekje.Web.ViewModels;
public class RegisterViewModel
{
    [Required]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }

    [Required]
    [Compare("Password", ErrorMessage = "Passwords komen niet overeen")]
    public string ConfirmPassword { get; set; }
}