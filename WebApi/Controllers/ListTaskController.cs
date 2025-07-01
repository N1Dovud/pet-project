using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Business.ListTasks;
using WebApi.Helpers;
using WebApi.Mappers;
using WebApi.Services.TaskServices;

namespace WebApi.Controllers;

[ApiController]
[Authorize]
[Route("api")]
public class ListTaskController(IListTaskService service) : Controller
{
    [HttpGet("tasks")]
    public async Task<IActionResult> GetTasks(long listId)
    {
        var id = this.GetUserId();
        if (id == null)
        {
            return this.Unauthorized();
        }

        ListTaskInfo? listTaskInfo = await service.GetAllTasksAsync(id.Value, listId);

        if (listTaskInfo == null)
        {
            return this.BadRequest();
        }

        return this.Ok(listTaskInfo.ToModel());
    }
}
