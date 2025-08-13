using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Common;
using WebApp.Mappers;
using WebApp.Models.Enums;
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
}
