using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Mappers;
using WebApp.Services.DatabaseService;

namespace WebApp.Controllers;

[Controller]
[Authorize]
public class ListTaskController(IListTaskWebApiService taskService) : Controller
{
    [Route("tasks")]
    public IActionResult GetListInfo([FromQuery] long listId)
    {
        if (listId <= 0)
        {
            return this.BadRequest("Invalid list ID");
        }

        var listInfo = taskService.GetListInfoAsync(listId).Result;

        if (listInfo == null)
        {
            return this.BadRequest();
        }

        return this.View("ListInfo", listInfo.ToModel());
    }
}
