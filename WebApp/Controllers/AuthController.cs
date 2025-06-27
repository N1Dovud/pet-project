using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers;
public class AuthController(UserManager<User> userManager) : Controller
{
    [Route("")]
    [Route("/")]
    public IActionResult Index()
    {
        return this.View("LandingPage");
    }

    [HttpGet("/sign-up")]
    public IActionResult SignUp()
    {
        return this.View();
    }

    [HttpPost("/sign-up")]
    public async Task<IActionResult> SignUp(SignUpViewModel model)
    {
        if (!this.ModelState.IsValid || model == null)
        {
            return this.View(model);
        }

        var user = new User
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            UserName = model.Username,
        };

        var result = await userManager.CreateAsync(user, model.Password);

        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                switch (error.Code)
                {
                    case "DuplicateUserName":
                        this.ModelState.AddModelError(nameof(model.Username), error.Description);
                        break;
                    case "PasswordTooShort":
                    case "PasswordRequiresDigit":
                    case "PasswordRequiredLower":
                    case "PasswordRequiresUpper":
                    case "PasswordRequiresNonAlphanumeric":
                        this.ModelState.AddModelError(nameof(model.Password), error.Description);
                        break;
                    case "InvalidUserName":
                        this.ModelState.AddModelError(nameof(model.Username), error.Description);
                        break;
                    default:
                        this.ModelState.AddModelError(string.Empty, error.Description);
                        break;
                }
            }

            return this.View(model);
        }

        return this.RedirectToAction("Home", "ToDoList");
    }

    [HttpGet("/sign-in")]
    public IActionResult SignIn()
    {

    }

    [HttpPost("/sign-in")]
    public IActionResult SignIn()
    {

    }
}
