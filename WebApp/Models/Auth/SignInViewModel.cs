using System.ComponentModel.DataAnnotations;

namespace WebApp.Models.Auth;

internal class SignInViewModel
{
    [Required]
    public string Username { get; set; } = default!;

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = default!;
}
