using System.Diagnostics.Eventing.Reader;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using WebApi.Business.ToDoLists;
using WebApi.Common;
using WebApi.Helpers;
using WebApi.Mappers;
using WebApi.Models.ToDoLists;
using WebApi.Services.ListServices;

namespace WebApi.Controllers;

[ApiController]
[Route("api")]
[Authorize]
internal class ToDoListController(IToDoListService listService, ILogger<ToDoListController> logger): ControllerBase
{
    [HttpGet("lists")]
    public async Task<IActionResult> GetLists()
    {
        var id = this.GetUserId();
        if (id == null)
        {
            return this.Unauthorized();
        }

        var work = await listService.GetAllToDoListsAsync(id.Value);
        if (work?.Result?.Status != ResultStatus.Success)
        {
            return this.ToHttpResponse(work?.Result!);
        }

        var listModels = work?.Data?.Select(l => l?.ToModel()).ToList();
        return this.Ok(listModels);
    }

    [HttpGet("list")]
    public async Task<IActionResult> GetList(long listId)
    {
        var id = this.GetUserId();
        if (id == null)
        {
            return this.Unauthorized();
        }

        var work = await listService.GetToDoListAsync(id.Value, listId);

        if (work?.Result?.Status != ResultStatus.Success)
        {
            return this.ToHttpResponse(work?.Result!);
        }

        return this.Ok(work?.Data?.ToModel());
    }

    [HttpPost("list")]
    public async Task<IActionResult> AddList([FromBody] ToDoListModel list)
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
        Result result = await listService.AddToDoListAsync(list.ToDomain());
        return this.ToHttpResponse(result);
    }

    [HttpDelete("list")]
    public async Task<IActionResult> DeleteList([FromQuery] long listId)
    {
        long? id = this.GetUserId();
        if (id == null)
        {
            return this.Unauthorized();
        }

        Result result = await listService.DeleteToDoListAsync(listId, id.Value);
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

        Result result = await listService.UpdateToDoListAsync(list.ToDomain(), id.Value);
        return this.ToHttpResponse(result);
    }
}
