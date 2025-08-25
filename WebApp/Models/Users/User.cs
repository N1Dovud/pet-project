using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace WebApp.Models.Users;

public class User : IdentityUser<long>
{
    [Required]
    public string FirstName { get; set; } = default!;

    [Required]
    public string LastName { get; set; } = default!;

    [Required]
    public override string? UserName { get; set; } = default!;

    [Required]
    public override string? Email { get; set; } = default!;
}
