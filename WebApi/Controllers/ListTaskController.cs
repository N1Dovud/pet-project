using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Business.ListTasks;
using WebApi.Helpers;
using WebApi.Mappers;
using WebApi.Models.ListTasks;
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

    [HttpPost("task")]
    public async Task<IActionResult> AddTask(TaskDetailsModel task, long listId)
    {
        var id = this.GetUserId();
        if (id == null)
        {
            return this.Unauthorized();
        }

        if (task == null)
        {
            return this.BadRequest();
        }

        var result = await service.AddTaskAsync(task.ToDomain(), id.Value, listId);

        return this.ToHttpResponse(result);
    }

    [HttpDelete("task")]
    public async Task<IActionResult> DeleteTask(long taskId)
    {
        var id = this.GetUserId();
        if (id == null)
        {
            return this.Unauthorized();
        }

        var result = await service.DeleteTaskAsync(taskId, id.Value);
        return this.ToHttpResponse(result);
    }

    [HttpPut("task")]
    public async Task<IActionResult> UpdateTask([FromBody] TaskDetailsModel task)
    {
        var id = this.GetUserId();
        if (id == null)
        {
            return this.Unauthorized();
        }

        var result = await service.UpdateTaskAsync(task.ToDomain(), id.Value);
        return this.ToHttpResponse(result);
    }

    [HttpGet("task")]
    public async Task<IActionResult> GetTask([FromQuery] long taskId)
    {
        var id = this.GetUserId();
        if (id == null)
        {
            return this.Unauthorized();
        }

        var task = await service.GetTaskAsync(id.Value, taskId);
        return this.Ok(task.ToModel());
    }
}
