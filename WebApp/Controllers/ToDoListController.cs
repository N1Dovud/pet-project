using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Business.ToDoLists;
using WebApp.Common;
using WebApp.Helpers;
using WebApp.Mappers;
using WebApp.Models.ToDoLists;
using WebApp.Services.ToDoListService;

namespace WebApp.Controllers;

[Controller]
[Authorize]
public class ToDoListController(IToDoListWebApiService service): Controller
{
    [Route("home")]
    public async Task<IActionResult> Home()
    {
        var work = await service.GetToDoListsAsync();
        if (work?.Result?.Status != ResultStatus.Success)
        {
            return this.ToHttpResponse(work?.Result ?? Result.Error());
        }

        return this.View(work?.Data?.Select(l => l?.ToDTO()).ToList());
    }

    [HttpGet("add-list")]
    public IActionResult AddList()
    {
        return this.View();
    }

    [HttpPost("add-list")]
    public async Task<IActionResult> AddList(ToDoListModel list)
    {
        if (list == null)
        {
            return this.BadRequest("List cannot be null");
        }

        if (!this.ModelState.IsValid)
        {
            return this.View(list);
        }

        Result result = await service.AddToDoListAsync(list.ToDomain());
        if (result?.Status != ResultStatus.Success)
        {
            return this.ToHttpResponse(result ?? Result.Error());
        }

        return this.RedirectToAction("Home", "ToDoList");
    }

    [HttpPost("delete-list")]
    public async Task<IActionResult> DeleteList(long id)
    {
        if (!this.ModelState.IsValid)
        {
            return this.BadRequest("You messed up!");
        }

        if (id == 0)
        {
            return this.RedirectToAction("Home", "ToDoList");
        }

        var result = await service.DeleteToDoListAsync(id);
        if (result?.Status != ResultStatus.Success)
        {
            return this.ToHttpResponse(result ?? Result.Error());
        }

        return this.RedirectToAction("Home", "ToDoList");
    }

    [HttpPost("update-list")]
    public async Task<IActionResult> UpdateList(ToDoListModel list)
    {
        if (list == null)
        {
            return this.BadRequest("List cannot be null");
        }

        if (!this.ModelState.IsValid)
        {
            return this.View(list);
        }

        Result result = await service.UpdateToDoListAsync(list.ToDomain());
        if (result?.Status != ResultStatus.Success)
        {
            return this.ToHttpResponse(result ?? Result.Error());
        }

        return this.RedirectToAction("Home", "ToDoList");
    }

    [HttpGet("update-list")]
    public async Task<IActionResult> UpdateList(long id)
    {
        if (!this.ModelState.IsValid && id == 0)
        {
            return this.BadRequest("Provide the id!");
        }

        var work = await service.GetToDoListAsync(id);

        if (work?.Result?.Status != ResultStatus.Success)
        {
            return this.ToHttpResponse(work?.Result ?? Result.Error());
        }

        return this.View(work?.Data?.ToModel());
    }
}
