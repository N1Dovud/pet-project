using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Business.ListTasks;
using WebApp.Common;
using WebApp.Mappers;
using WebApp.Models.Enums;
using WebApp.Models.Helpers;
using WebApp.Models.Tags;
using WebApp.Services.ListTaskService;
using WebApp.Services.TagService;

namespace WebApp.Controllers;

[Controller]
[Authorize]
public class TagController(ITagWebApiService tagservice) : Controller
{
    [HttpGet("tags")]
    public async Task<IActionResult> GetTags()
    {
        var work = await tagservice.GetAllTags();
        if (work?.Result?.Status != ResultStatus.Success)
        {
            return this.BadRequest(work?.Result?.Message);
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
            return this.BadRequest(work?.Result?.Message);
        }

        return this.View("TasksByTag", new TasksByTagViewModel
        {
            TaskSummaries = work?.Data?.Select(t => t?.ToModel()).ToList(),
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

        var work = await tagservice.AddTag(taskId, tagName);
        if (work?.Status != ResultStatus.Success)
        {
            return this.BadRequest(work?.Message);
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

        var work = await tagservice.DeleteTag(taskId, tagId);
        if (work?.Status != ResultStatus.Success)
        {
            return this.BadRequest(work?.Message);
        }

        return this.Redirect(returnUrl);
    }
}
