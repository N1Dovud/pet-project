using System.Globalization;
using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Business.ListTasks;
using WebApp.Common;
using WebApp.Helpers;
using WebApp.Mappers;
using WebApp.Models.Helpers;
using WebApp.Models.Helpers.Enums;
using WebApp.Models.ListTasks;
using WebApp.Services.ListTaskService;

namespace WebApp.Controllers;

[Controller]
[Authorize]
public class ListTaskController(IListTaskWebApiService taskService): Controller
{
    [HttpGet("tasks")]
    public async Task<IActionResult> GetListInfo([FromQuery] long listId)
    {
        if (listId <= 0)
        {
            return this.BadRequest("Invalid list ID");
        }

        var work = await taskService.GetListInfoAsync(listId);

        if (work?.Result?.Status != ResultStatus.Success)
        {
            return this.ToHttpResponse(work?.Result ?? Result.Error());
        }

        return this.View("ListInfo", work?.Data?.ToModel());
    }

    [HttpGet("edit-task")]
    public IActionResult EditTask([FromQuery] long taskId, string? returnUrl)
    {
        if (taskId <= 0)
        {
            return this.BadRequest("Invalid task ID");
        }

        var work = taskService.GetTaskDetailsAsync(taskId).Result;
        if (work?.Result?.Status != ResultStatus.Success)
        {
            return this.ToHttpResponse(work?.Result ?? Result.Error());
        }

        return this.View("EditTask", new TaskViewModel
        {
            ReturnUrl = returnUrl,
            TaskDetails = work?.Data?.ToModel(),
        });
    }

    [HttpPost("edit-task")]
    public async Task<IActionResult> EditTask(TaskViewModel model)
    {
        if (!this.ModelState.IsValid)
        {
            return this.BadRequest("Model is faulty!");
        }

        var result = await taskService.EditTaskAsync(model?.TaskDetails?.ToDomain());
        if (result.Status != ResultStatus.Success)
        {
            return this.ToHttpResponse(result);
        }

        return this.Redirect(model?.ReturnUrl ?? "/");
    }

    [HttpGet("task")]
    public async Task<IActionResult> GetTaskDetails([FromQuery] long taskId, string? returnUrl, bool isOwner)
    {
        if (!this.ModelState.IsValid)
        {
            return this.BadRequest("Model is faulty");
        }

        if (taskId <= 0)
        {
            return this.BadRequest("Invalid task ID");
        }

        var work = await taskService.GetTaskDetailsAsync(taskId);

        if (work?.Result?.Status != ResultStatus.Success)
        {
            return this.ToHttpResponse(work?.Result ?? Result.Error());
        }

        return this.View("TaskDetails", new TaskViewModel
        {
            ReturnUrl = returnUrl,
            TaskDetails = work?.Data?.ToModel(),
            IsOwner = isOwner,
        });
    }

    [HttpGet("add-task")]
    public IActionResult AddTask([FromQuery] long listId)
    {
        if (!this.ModelState.IsValid)
        {
            return this.BadRequest("Model is faulty");
        }

        if (listId <= 0)
        {
            return this.BadRequest("Invalid list ID");
        }

        var model = new AddTaskViewModel
        {
            ListId = listId,
            TaskDetails = new TaskDetailsModel([], []),
        };
        return this.View("AddTask", model);
    }

    [HttpPost("add-task")]
    public async Task<IActionResult> AddTask(AddTaskViewModel model)
    {
        if (!this.ModelState.IsValid)
        {
            return this.BadRequest();
        }

        Result result = await taskService.AddTaskAsync(model?.TaskDetails?.ToDomain(), model?.ListId);

        if (result.Status != ResultStatus.Success)
        {
            return this.ToHttpResponse(result);
        }

        return this.RedirectToAction("GetListInfo", new { model?.ListId });
    }

    [HttpPost("delete-task")]
    public async Task<IActionResult> DeleteTask(long taskId, string? returnUrl)
    {
        if (!this.ModelState.IsValid)
        {
            return this.BadRequest();
        }

        if (taskId <= 0)
        {
            return this.BadRequest();
        }

        var result = await taskService.DeleteTaskAsync(taskId);
        if (result.Status != ResultStatus.Success)
        {
            return this.ToHttpResponse(result);
        }

        return this.Redirect(returnUrl ?? "/");
    }

    [HttpGet("overdue")]
    public async Task<IActionResult> GetOverdueTasks()
    {
        var work = await taskService.GetOverdueTasksAsync();
        if (work?.Result?.Status != ResultStatus.Success)
        {
            return this.ToHttpResponse(work?.Result ?? Result.Error());
        }

        return this.View("OverdueTasks", work?.Data?.Select(t => t?.ToModel()).ToList());
    }

    [HttpGet("assigned")]
    public async Task<IActionResult> GetAssignedTasks(StatusFilter filter = StatusFilter.Active, SortField? sortBy = null, bool descending = false)
    {
        if (!this.ModelState.IsValid)
        {
            return this.BadRequest("Invalid filter or sort parameters");
        }

        var work = await taskService.GetAssignedTasksAsync(filter, sortBy, descending);
        if (work?.Result?.Status != ResultStatus.Success)
        {
            return this.ToHttpResponse(work?.Result ?? Result.Error());
        }

        return this.View("AssignedTasks", new AssignedTasksModel(work?.Data?.Select(t => t?.ToModel()) ?? [])
        {
            Filter = filter,
            SortBy = sortBy,
            Descending = descending,
        });
    }

    [HttpPost("task/update-task-status")]
    public async Task<IActionResult> UpdateTaskStatus(EditTaskStatusModel model, string? returnUrl)
    {
        if (!this.ModelState.IsValid)
        {
            return this.BadRequest();
        }

        var result = await taskService.EditTaskStatusAsync(model.ToDomain());

        if (result.Status != ResultStatus.Success)
        {
            return this.ToHttpResponse(result);
        }

        return this.Redirect(returnUrl ?? "/");
    }

    [HttpGet("task-search")]
    public async Task<IActionResult> SearchTasks(SearchFields searchType, string queryValue)
    {
        if (!this.ModelState.IsValid)
        {
            return this.BadRequest("Invalid search parameters");
        }

        ResultWithData<List<TaskSummary?>?> work;
        DateTime date;
        if (searchType != SearchFields.Title)
        {
            if (!DateTime.TryParse(queryValue, CultureInfo.InvariantCulture, out date))
            {
                return this.BadRequest("Invalid date provided");
            }

            work = await taskService.SearchTasksAsync(searchType, date);
        }
        else
        {
            work = await taskService.SearchTasksAsync(searchType, queryValue);
        }

        if (work?.Result?.Status != ResultStatus.Success)
        {
            return this.ToHttpResponse(work?.Result ?? Result.Error());
        }

        return this.View("TaskSearchResults", new TaskSearchModel(work.Data?.Select(t => t?.ToModel()) ?? [])
        {
            ReturnUrl = $"/task-search?searchType={searchType}&queryValue={queryValue}",
        });
    }
}
