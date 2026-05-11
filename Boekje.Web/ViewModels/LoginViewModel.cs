using System.ComponentModel.DataAnnotations;

namespace Boekje.Web.ViewModels;

public class LoginViewModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }
}