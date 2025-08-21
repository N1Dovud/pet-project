using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Common;
using WebApi.Helpers;
using WebApi.Mappers;
using WebApi.Models.Helpers;
using WebApi.Services.TagsServices;

namespace WebApi.Controllers;

[ApiController]
[Authorize]
[Route("api")]
public class TagController(ITagService tagService) : ControllerBase
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
            return this.BadRequest();
        }

        return this.Ok(work?.Data?.Select(t => t.ToModel()));

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
            return this.BadRequest();
        }

        return this.Ok(work?.Data?.Select(t => t.ToModel()));
    }

    [HttpPost("add-tag")]
    public async Task<IActionResult> AddTag(AddTagModel model)
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

        var work = await tagService.AddTag(id.Value, model.TagName, model.TaskId);
        if (work?.Status != ResultStatus.Success)
        {
            return this.BadRequest();
        }

        return this.Ok();
    }

    [HttpPost("delete-tag")]
    public async Task<IActionResult> DeleteTag(DeleteTagModel model)
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

        var work = await tagService.DeleteTag(id.Value, model.TagId, model.TaskId);
        if (work?.Status != ResultStatus.Success)
        {
            return this.BadRequest();
        }

        return this.Ok();
    }
}
