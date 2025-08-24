using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Business.ListTasks;
using WebApi.Common;
using WebApi.Helpers;
using WebApi.Mappers;
using WebApi.Models.Helpers;
using WebApi.Models.ListTasks;
using WebApi.Services.TaskServices;

namespace WebApi.Controllers;

[ApiController]
[Authorize]
[Route("api")]
internal class ListTaskController(IListTaskService service): ControllerBase
{
    [HttpGet("tasks")]
    public async Task<IActionResult> GetTasks(long listId)
    {
        var id = this.GetUserId();
        if (id == null)
        {
            return this.Unauthorized();
        }

        var work = await service.GetAllTasksAsync(id.Value, listId);

        if (work?.Result?.Status != ResultStatus.Success)
        {
            return this.ToHttpResponse(work?.Result!);
        }

        return this.Ok(work?.Data?.ToModel());
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

        var work = await service.GetTaskAsync(id.Value, taskId);
        if (work?.Result?.Status != ResultStatus.Success)
        {
            return this.ToHttpResponse(work?.Result!);
        }

        return this.Ok(work?.Data?.ToModel());
    }

    [HttpGet("overdue")]
    public async Task<IActionResult> GetOverdueTasks()
    {
        var id = this.GetUserId();
        if (id == null)
        {
            return this.Unauthorized();
        }

        var work = await service.GetOverdueTasks(id.Value);
        if (work?.Result?.Status != ResultStatus.Success)
        {
            return this.ToHttpResponse(work?.Result!);
        }

        return this.Ok(work?.Data?.Select(t => t.ToModel()));
    }

    [HttpGet("assigned")]
    public async Task<IActionResult> GetAssignedTasks(StatusFilter filter, SortField? sortBy, bool descending = false)
    {
        var id = this.GetUserId();
        if (id == null)
        {
            return this.Unauthorized();
        }

        var work = await service.GetAssignedTasks(id.Value, filter, sortBy, descending);
        if (work?.Result?.Status != ResultStatus.Success)
        {
            return this.ToHttpResponse(work?.Result!);
        }

        return this.Ok(work?.Data?.Select(t => t.ToModel()));
    }

    [HttpPost("status-update")]
    public async Task<IActionResult> EditTaskStatus(EditTaskStatusModel model)
    {
        var id = this.GetUserId();
        if (id == null)
        {
            return this.Unauthorized();
        }

        if (model == null)
        {
            return this.BadRequest();
        }

        var result = await service.EditTaskStatusAsync(id.Value, model.ToDomain()!);

        return this.ToHttpResponse(result);
    }

    [HttpGet("task-search")]
    public async Task<IActionResult> SearchTasks(SearchFields searchType, string queryValue)
    {
        if (!this.ModelState.IsValid)
        {
            return this.BadRequest("Invalid search parameters");
        }

        var id = this.GetUserId();

        if (id == null)
        {
            return this.Unauthorized();
        }

        ResultWithData<List<TaskSummary?>?> work;
        DateTime date;
        if (searchType != SearchFields.Title)
        {
            if (!DateTime.TryParse(queryValue, out date))
            {
                return this.BadRequest("Invalid date provided");
            }

            work = await service.SearchTasksAsync(id.Value, searchType, date);
        }
        else
        {
            work = await service.SearchTasksAsync(id.Value, searchType, queryValue);
        }

        if (work?.Result?.Status != ResultStatus.Success)
        {
            return this.ToHttpResponse(work?.Result!);
        }

        return this.Ok(work.Data?.Select(t => t.ToModel()).ToList());
    }
}
