using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
public class ListTaskController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
