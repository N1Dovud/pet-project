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
    [HttpGet("getlists")]
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

    [HttpPost("addlist")]
    public async Task<ActionResult> AddList([FromBody] ToDoListModel list)
    {
        bool success = await dbService.AddToDoListAsync(list.ToDomain());
        if (success)
        {
            return this.Ok();
        }

        return this.BadRequest();
    }
}
