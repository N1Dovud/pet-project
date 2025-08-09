using System.Globalization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Business.ListTasks;
using WebApp.Common;
using WebApp.Mappers;
using WebApp.Models.Helpers;
using WebApp.Models.Helpers.Enums;
using WebApp.Models.ListTasks;
using WebApp.Services.ListTaskService;

namespace WebApp.Controllers;

[Controller]
[Authorize]
public class ListTaskController(IListTaskWebApiService taskService) : Controller
{
    [HttpGet("tasks")]
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

    [HttpGet("edit-task")]
    public IActionResult EditTask([FromQuery] long taskId, long listId)
    {
        if (taskId <= 0)
        {
            return this.BadRequest("Invalid task ID");
        }

        var taskDetails = taskService.GetTaskDetailsAsync(taskId).Result;
        if (taskDetails == null)
        {
            return this.NotFound();
        }

        return this.View("EditTask", new EditTaskViewModel
        {
            listId = listId,
            taskDetails = taskDetails.ToModel(),
        });
    }

    [HttpPost("edit-task")]
    public async Task<IActionResult> EditTask(EditTaskViewModel model)
    {
        if (!this.ModelState.IsValid)
        {
            return this.BadRequest("Model is faulty!");
        }

        var result = await taskService.EditTaskAsync(model?.taskDetails?.ToDomain());
        if (result.Status == ResultStatus.Success)
        {
            if (model?.listId == 0)
            {
                return this.Redirect("overdue");
            }

            return this.RedirectToAction("GetListInfo", new { listId = model?.listId });
        }

        return this.BadRequest("Something went off");
    }

    [HttpGet("task")]
    public async Task<IActionResult> GetTaskDetails([FromQuery] long taskId, long listId)
    {
        if (taskId <= 0)
        {
            return this.BadRequest("Invalid task ID");
        }

        var taskDetails = await taskService.GetTaskDetailsAsync(taskId);

        if (taskDetails == null)
        {
            return this.BadRequest();
        }

        return this.View("TaskDetails", new EditTaskViewModel
        {
            listId = listId,
            taskDetails = taskDetails.ToModel(),
        });
    }

    [HttpGet("add-task")]
    public async Task<IActionResult> AddTask([FromQuery] long listId)
    {
        if (listId <= 0)
        {
            return this.BadRequest("Invalid list ID");
        }

        var model = new AddTaskViewModel
        {
            listId = listId,
            taskDetails = new TaskDetailsModel(),
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

        Result result = await taskService.AddTaskAsync(model.taskDetails.ToDomain(), model.listId);

        if (result.Status == ResultStatus.Success)
        {
            return this.RedirectToAction("GetListInfo", new { model.listId });
        }

        return this.BadRequest();
    }

    [HttpPost("delete-task")]
    public async Task<IActionResult> DeleteTask(long taskId, long listId)
    {
        if (taskId <= 0)
        {
            return this.BadRequest();
        }

        var result = await taskService.DeleteTaskAsync(taskId);
        if (result.Status == ResultStatus.Unauthorized)
        {
            this.TempData["Error"] = result.Message ?? "Sign in please";
            return this.RedirectToAction("SignIn", "Auth");
        }

        if (result.Status == ResultStatus.Success)
        {
            if (listId == 0)
            {
                return this.Redirect("overdue");
            }

            return this.Redirect(this.Url.Action("GetListInfo", new { listId }));
        }

        this.TempData["Error"] = result.Message ?? "Failed to delete task";
        return this.RedirectToAction("Home", "ToDoList");
    }

    [HttpGet("overdue")]
    public async Task<IActionResult> GetOverdueTasks()
    {
        var tasks = await taskService.GetOverdueTasksAsync();

        return this.View("OverdueTasks", tasks.Select(t => t.ToModel()).ToList());
    }

    [HttpGet("assigned")]
    public async Task<IActionResult> GetAssignedTasks(StatusFilter filter = StatusFilter.Active, SortField? sortBy = null, bool descending = false)
    {
        if (!this.ModelState.IsValid)
        {
            return this.BadRequest("Invalid filter or sort parameters");
        }

        var tasks = await taskService.GetAssignedTasksAsync(filter, sortBy, descending);
        return this.View("AssignedTasks", new AssignedTasksModel
        {
            Filter = filter,
            SortBy = sortBy,
            Descending = descending,
            Tasks = tasks.Select(t => t.ToModel()).ToList(),
        });
    }

    [HttpPost("task/update-task-status")]
    public async Task<IActionResult> UpdateTaskStatus(EditTaskStatusModel model)
    {
        if (!this.ModelState.IsValid)
        {
            return this.BadRequest();
        }

        var result = await taskService.EditTaskStatusAsync(model.ToDomain());

        if (result.Status == ResultStatus.Success)
        {
            return this.Redirect("/assigned");
        }

        return this.BadRequest(result.Message);
    }

    [HttpGet("task-search")]
    public async Task<IActionResult> SearchTasks(SearchFields searchType, string queryValue)
    {
        if (!this.ModelState.IsValid)
        {
            return this.BadRequest("Invalid search parameters");
        }

        ResultWithData<List<TaskSummary?>?> result;
        DateTime date;
        if (searchType != SearchFields.Title)
        {
            if (!DateTime.TryParse(queryValue, out date))
            {
                return this.BadRequest("Invalid date provided");
            }

            result = await taskService.SearchTasksAsync<DateTime>(searchType, date);
        }
        else
        {
            result = await taskService.SearchTasksAsync<string>(searchType, queryValue);
        }

        if (result.Result?.Status != ResultStatus.Success)
        {
            return this.BadRequest(result.Result?.Message);
        }

        return this.View("TaskSearchResults", result.Data?.Select(t => t.ToModel()).ToList());
    }
}
