using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Common;
using WebApi.Helpers;
using WebApi.Mappers;
using WebApi.Models.Helpers;
using WebApi.Services.TagServices;

namespace WebApi.Controllers;

[ApiController]
[Authorize]
[Route("api")]
public class TagController(ITagService tagService): ControllerBase
{
    [HttpGet("tags")]
    public async Task<IActionResult> GetAllTags()
    {
        var id = this.GetUserId();
        if (id == null)
        {
            return this.Unauthorized();
        }

        var work = await tagService.GetAllTags(id.Value);
        if (work?.Result?.Status != ResultStatus.Success)
        {
            return this.ToHttpResponse(work?.Result!);
        }

        return this.Ok(work?.Data?.Select(t => t?.ToModel()));
    }

    [HttpGet("tag")]
    public async Task<IActionResult> GetTasksByTag(long tagId)
    {
        var id = this.GetUserId();
        if (id == null)
        {
            return this.Unauthorized();
        }

        var work = await tagService.GetTasksByTag(tagId, id.Value);
        if (work?.Result?.Status != ResultStatus.Success)
        {
            return this.ToHttpResponse(work?.Result!);
        }

        return this.Ok(work?.Data?.Select(t => t?.ToModel()));
    }

    [HttpPost("add-tag")]
    public async Task<IActionResult> AddTag(AddTagModel model)
    {
        if (!this.ModelState.IsValid || model == null)
        {
            return this.BadRequest();
        }

        var id = this.GetUserId();
        if (id == null)
        {
            return this.Unauthorized();
        }

        var result = await tagService.AddTag(id.Value, model.TagName, model.TaskId);
        return this.ToHttpResponse(result);
    }

    [HttpDelete("delete-tag")]
    public async Task<IActionResult> DeleteTag(long taskId, long tagId)
    {
        if (!this.ModelState.IsValid)
        {
            return this.BadRequest();
        }

        var id = this.GetUserId();
        if (id == null)
        {
            return this.Unauthorized();
        }

        var result = await tagService.DeleteTag(id.Value, tagId, taskId);
        return this.ToHttpResponse(result);
    }
}
