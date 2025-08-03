using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Mappers;
using WebApp.Models.Helpers;
using WebApp.Models.ListTasks;
using WebApp.Services.DatabaseService;

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
            return this.Redirect(this.Url.Action("GetListInfo", new { listId }));
        }

        this.TempData["Error"] = result.Message ?? "Failed to delete task";
        return this.RedirectToAction("Home", "ToDoList");
    }
}
