using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Services.DatabaseService;

namespace WebApp.Controllers;

[Controller]
[Authorize]
public class ListTaskController(IListTaskWebApiService taskService) : Controller
{
    [Route("tasks")]
    public IActionResult Index([FromQuery] long listId)
    {
        if (listId <= 0)
        {
            return this.BadRequest("Invalid list ID");
        }

        var listInfo = taskService.GetListInfoAsync(listId).Result;
        return View();
    }
}
