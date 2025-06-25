using System.Diagnostics.Eventing.Reader;
using Microsoft.AspNetCore.Mvc;
using WebApi.Business;
using WebApi.Helpers;
using WebApi.Mappers;
using WebApi.Models;
using WebApi.Services.Authentication;
using WebApi.Services.DatabaseService;

namespace WebApi.Controllers;

[ApiController]
[Route("api")]
public class ToDoListController(IToDoListDatabaseService dbService, ILogger<ToDoListController> logger, IAuthenticationService authenticator) : ControllerBase
{
    [HttpGet("lists")]
    public async Task<ActionResult<List<ToDoListModel>>> GetLists([FromQuery] long userId)
    {
        var id = authenticator.Authenticate(userId);
        if (id == null)
        {
            logger.LogWarning("Authentication failed for userId: {UserId}", userId);
            return this.Unauthorized();
        }

        List<ToDoList> lists = await dbService.GetAllToDoListsAsync(id.Value);
        var listModels = lists.Select(l => l.ToModel()).ToList();
        return this.Ok(listModels);
    }

    [HttpPost("list")]
    public async Task<ActionResult> AddList([FromBody] ToDoListModel list)
    {
        Result result = await dbService.AddToDoListAsync(list.ToDomain());

        if (result.Status == ResultStatus.Success)
        {
            return this.StatusCode(201);
        }

        return this.BadRequest();
    }

    [HttpDelete("list")]
    public async Task<IActionResult> DeleteList([FromQuery] long listId, [FromQuery] long ownerId)
    {
        long? id = authenticator.Authenticate(ownerId);
        if (id == null)
        {
            return this.Forbid("Authentication problem");
        }

        Result result = await dbService.DeleteToDoListAsync(listId, id.Value);
        return this.ToHttpResponse(result);
    }

    [HttpPut("list")]
    public async Task<IActionResult> UpdateList([FromBody] ToDoListModel list, [FromQuery] long userId)
    {
        Result result = await dbService.UpdateToDoListAsync(list.ToDomain(), userId);
        return this.ToHttpResponse(result);
    }
}
