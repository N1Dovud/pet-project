using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models.Auth;
using WebApp.Models.Users;
using WebApp.Services.AuthenticationService;

namespace WebApp.Controllers;
public class AuthController(UserManager<User> userManager, IJwtService jwtService): Controller
{
    [Route("")]
    public IActionResult Index()
    {
        if (this.User.Identity?.IsAuthenticated == true)
        {
            return this.RedirectToAction("Home", "ToDoList");
        }

        return this.View("LandingPage");
    }

    [HttpGet("/sign-up")]
    public IActionResult SignUp()
    {
        if (this.User.Identity?.IsAuthenticated == true)
        {
            return this.RedirectToAction("Home", "ToDoList");
        }

        return this.View("SignUp");
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
            Email = model.Email,
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

        jwtService.SetJwtToken(this.HttpContext, user);

        return this.RedirectToAction("Home", "ToDoList");
    }

    [HttpGet("/sign-in")]
    public IActionResult SignIn()
    {
        if (this.User.Identity?.IsAuthenticated == true)
        {
            return this.RedirectToAction("Home", "ToDoList");
        }

        return this.View("SignIn");
    }

    [HttpPost("/sign-in")]
    public async Task<IActionResult> SignIn(SignInViewModel model)
    {
        var user = await userManager.FindByNameAsync(model?.Username ?? string.Empty);
        if (user != null && await userManager.CheckPasswordAsync(user, model?.Password ?? string.Empty))
        {
            jwtService.SetJwtToken(this.HttpContext, user);
            return this.RedirectToAction("Home", "ToDoList");
        }

        this.ModelState.AddModelError(string.Empty, "Login or Password is wrong");
        return this.View();
    }

    [HttpPost("/logout")]
    public IActionResult Logout()
    {
        jwtService.ClearJwtToken(this.HttpContext);
        return this.RedirectToAction("Index");
    }
}
