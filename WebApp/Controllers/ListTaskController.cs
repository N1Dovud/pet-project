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
    public IActionResult EditTask([FromQuery] long taskId)
    {
        if (taskId <= 0)
        {
            return this.BadRequest("Invalid task ID");
        }

        var taskDetails = taskService.GetListInfoAsync(taskId).Result;
        if (taskDetails == null)
        {
            return this.NotFound();
        }

        return this.View("EditTask", taskDetails.ToModel());
    }

    [HttpGet("task")]
    public async Task<IActionResult> GetTaskDetails([FromQuery] long taskId)
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

        return this.View("TaskDetails", taskDetails.ToModel());
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
            return this.RedirectToAction("GetListInfo", model.listId);
        }

        return this.BadRequest();
    }

    [HttpPost("delete-task")]
    public async Task<IActionResult> DeleteTask([FromQuery] long taskId)
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
            return this.RedirectToAction("Home", "ToDoList");
        }

        this.TempData["Error"] = result.Message ?? "Failed to delete list";
        return this.RedirectToAction("Home", "ToDoList");
    }
}
