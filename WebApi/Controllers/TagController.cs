using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Common;
using WebApi.Helpers;
using WebApi.Mappers;
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
        if (work?.Result?.Status == ResultStatus.Success)
        {
            return this.BadRequest();
        }

        return this.Ok(work?.Data?.Select(t => t.ToModel()));

    }
}
