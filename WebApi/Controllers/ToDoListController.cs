using System.Diagnostics.Eventing.Reader;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using WebApi.Business.ToDoLists;
using WebApi.Helpers;
using WebApi.Mappers;
using WebApi.Models.ToDoLists;
using WebApi.Services.DatabaseService;

namespace WebApi.Controllers;

[ApiController]
[Route("api")]
[Authorize]
public class ToDoListController(IToDoListDatabaseService dbService, ILogger<ToDoListController> logger) : ControllerBase
{
    [HttpGet("lists")]
    public async Task<ActionResult<List<ToDoListModel>>> GetLists()
    {
        var id = this.GetUserId();
        if (id == null)
        {
            logger.LogWarning("Authentication failed for userId: {UserId}", id);
            return this.Unauthorized();
        }

        List<ToDoList> lists = await dbService.GetAllToDoListsAsync(id.Value);
        var listModels = lists.Select(l => l.ToModel()).ToList();
        return this.Ok(listModels);
    }

    [HttpGet("list")]
    public async Task<ActionResult<List<ToDoListModel>>> GetList(long listId)
    {
        var id = this.GetUserId();
        if (id == null)
        {
            logger.LogWarning("Authentication failed for userId: {UserId}", id);
            return this.Unauthorized();
        }

        ToDoList? list = await dbService.GetToDoListAsync(id.Value, listId);

        if (list == null)
        {
            return this.BadRequest();
        }

        return this.Ok(list.ToModel());
    }

    [HttpPost("list")]
    public async Task<ActionResult> AddList([FromBody] ToDoListModel list)
    {
        if (list == null)
        {
            return this.BadRequest("list empty");
        }

        var id = this.GetUserId();
        if (id == null)
        {
            return this.Unauthorized();
        }

        list.OwnerId = id.Value;
        Result result = await dbService.AddToDoListAsync(list.ToDomain());

        if (result.Status == ResultStatus.Success)
        {
            return this.StatusCode(201);
        }

        return this.BadRequest();
    }

    [HttpDelete("list")]
    public async Task<IActionResult> DeleteList([FromQuery] long listId)
    {
        long? id = this.GetUserId();
        if (id == null)
        {
            return this.Unauthorized();
        }

        Result result = await dbService.DeleteToDoListAsync(listId, id.Value);
        return this.ToHttpResponse(result);
    }

    [HttpPut("list")]
    public async Task<IActionResult> UpdateList([FromBody] ToDoListModel list)
    {
        var id = this.GetUserId();
        if (id == null)
        {
            return this.Unauthorized();
        }

        Result result = await dbService.UpdateToDoListAsync(list.ToDomain(), id.Value);
        return this.ToHttpResponse(result);
    }
}
