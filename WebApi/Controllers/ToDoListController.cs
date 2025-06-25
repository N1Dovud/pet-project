using System.Diagnostics.Eventing.Reader;
using Microsoft.AspNetCore.Mvc;
using WebApi.Business;
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
        bool success = await dbService.AddToDoListAsync(list.ToDomain());
        if (success)
        {
            return this.Ok();
        }

        return this.BadRequest();
    }

    [HttpDelete("list")]
    public async Task<ActionResult> DeleteList([FromQuery] long listId, [FromQuery] long ownerId)
    {
        long? id = authenticator.Authenticate(ownerId);
        if (id == null)
        {
            return this.Forbid("Authentication problem");
        }

        DeleteResult result = await dbService.DeleteToDoListAsync(listId, id.Value);
        return result switch
        {
            DeleteResult.Success => this.NoContent(),
            DeleteResult.NotFound => this.NotFound("List not found"),
            DeleteResult.Forbidden => this.StatusCode(403, "You are not allowed to delete this list"),
            _ => this.StatusCode(500, "An unexpected error occurred")
        };
    }
}
