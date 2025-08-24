using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Common;
using WebApp.Helpers;
using WebApp.Mappers;
using WebApp.Models.Helpers;
using WebApp.Models.Tags;
using WebApp.Services.TagService;

namespace WebApp.Controllers;

[Controller]
[Authorize]
internal class TagController(ITagWebApiService tagservice): Controller
{
    [HttpGet("tags")]
    public async Task<IActionResult> GetTags()
    {
        var work = await tagservice.GetAllTags();
        if (work?.Result?.Status != ResultStatus.Success)
        {
            return this.ToHttpResponse(work?.Result ?? Result.Error());
        }

        return this.View(work?.Data?.Select(t => t?.ToModel()).ToList());
    }

    [HttpGet("tag")]
    public async Task<IActionResult> GetTasksByTag(long tagId, string tagName)
    {
        if (!this.ModelState.IsValid)
        {
            return this.BadRequest("went wrong");
        }

        var work = await tagservice.GetTasksByTag(tagId);
        if (work?.Result?.Status != ResultStatus.Success)
        {
            return this.ToHttpResponse(work?.Result ?? Result.Error());
        }

        return this.View("TasksByTag", new TasksByTagViewModel(work?.Data?.Select(t => t?.ToModel()) ?? [])
        {
            Tag = new TagModel
            {
                Id = tagId,
                Name = tagName,
            },
        });
    }

    [HttpPost("add-tag")]
    public async Task<IActionResult> AddTag(long taskId, string tagName, string returnUrl)
    {
        if (!this.ModelState.IsValid || taskId == 0)
        {
            return this.BadRequest("went wrong");
        }

        var result = await tagservice.AddTag(taskId, tagName);
        if (result?.Status != ResultStatus.Success)
        {
            return this.ToHttpResponse(result ?? Result.Error());
        }

        return this.Redirect(returnUrl);
    }

    [HttpPost("delete-tag")]
    public async Task<IActionResult> DeleteTag(long taskId, long tagId, string returnUrl)
    {
        if (!this.ModelState.IsValid)
        {
            return this.BadRequest("went wrong");
        }

        var result = await tagservice.DeleteTag(taskId, tagId);
        if (result?.Status != ResultStatus.Success)
        {
            return this.ToHttpResponse(result ?? Result.Error());
        }

        return this.Redirect(returnUrl);
    }
}
