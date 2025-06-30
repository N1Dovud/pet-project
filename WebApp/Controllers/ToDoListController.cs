using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Business;
using WebApp.Mappers;
using WebApp.Models;
using WebApp.Services.DatabaseService;

namespace WebApp.Controllers;

[Controller]
[Authorize]
public class ToDoListController(IToDoListWebApiService service) : Controller
{
    [Route("home")]
    public async Task<IActionResult> Home()
    {
        List<ToDoList?>? lists = await service.GetToDoLists();
        if (lists == null)
        {
            return this.RedirectToAction("Index", "Auth");
        }

        return this.View(lists.Select(l => l?.ToDTO()).ToList());
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
        if (result.Status == ResultStatus.Success)
        {
            return this.RedirectToAction("Home", "ToDoList");
        }

        if (result.Status == ResultStatus.Forbidden)
        {
            return this.RedirectToAction("SignIn", "Auth");
        }

        this.ViewBag.Error = result.Message ?? "Failed to create list";
        return this.View(list);
    }

    [HttpPost("delete-list")]
    public async Task<IActionResult> DeleteList(long id)
    {
        if (id == 0)
        {
            return this.RedirectToAction("Home", "ToDoList");
        }

        var result = await service.DeleteToDoListAsync(id);
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
