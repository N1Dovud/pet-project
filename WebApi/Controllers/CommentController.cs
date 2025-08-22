using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Common;
using WebApi.Helpers;
using WebApi.Services.CommentServices;

namespace WebApi.Controllers;

[ApiController]
[Authorize]
[Route("api")]
public class CommentController(ICommentService service) : ControllerBase
{

    [HttpPost("add-comment")]
    public async Task<IActionResult> AddComment([FromBody] string note, [FromBody] long taskId)
    {
        if (!this.ModelState.IsValid)
        {
            return this.BadRequest("Wrong input");
        }

        var id = this.GetUserId();
        if (id == null)
        {
            return this.Unauthorized();
        }

        var result = await service.AddComment(id.Value, taskId, note);
        if (result.Status != ResultStatus.Success)
        {
            return this.BadRequest(result.Message);
        }

        return this.Ok();
    }

    [HttpPost("delete-comment")]
    public async Task<IActionResult> DeleteComment(long commentId)
    {
        if (!this.ModelState.IsValid)
        {
            return this.BadRequest("Wrong input");
        }

        var id = this.GetUserId();
        if (id == null)
        {
            return this.Unauthorized();
        }

        var result = await service.DeleteComment(id.Value, commentId);
        if (result.Status != ResultStatus.Success)
        {
            return this.BadRequest(result.Message);
        }

        return this.Ok();
    }

    [HttpPost("edit-comment")]
    public async Task<IActionResult> EditComment(long commentId, string note)
    {
        if (!this.ModelState.IsValid)
        {
            return this.BadRequest("Wrong input");
        }

        var id = this.GetUserId();
        if (id == null)
        {
            return this.Unauthorized();
        }

        var result = await service.EditComment(id.Value, commentId, note);
        if (result.Status != ResultStatus.Success)
        {
            return this.BadRequest(result.Message);
        }

        return this.Ok();
    }
}
