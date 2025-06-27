using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Business;
using WebApp.Mappers;
using WebApp.Services.DatabaseService;

namespace WebApp.Controllers;

[Controller]
[Authorize]
public class ToDoListController(IToDoListWebApiService service) : Controller
{
    [Route("/")]
    [Route("")]
    public async Task<IActionResult> Home()
    {
        List<ToDoList?>? lists = await service.GetToDoLists();
        if (lists == null)
        {
            return RedirectToAction("Home", "SignIn");
        }

        return this.View(lists.Select(l => l?.ToDTO()).ToList());
    }
}
