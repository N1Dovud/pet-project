using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Common;
using WebApi.Helpers;
using WebApi.Models.Helpers;
using WebApi.Services.CommentServices;

namespace WebApi.Controllers;

[ApiController]
[Authorize]
[Route("api")]
public class CommentController(ICommentService service) : ControllerBase
{

    [HttpPost("add-comment")]
    public async Task<IActionResult> AddComment(AddCommentModel model)
    {
        if (!this.ModelState.IsValid || model == null)
        {
            return this.BadRequest("Wrong input");
        }

        var id = this.GetUserId();
        if (id == null)
        {
            return this.Unauthorized();
        }

        var result = await service.AddComment(id.Value, model.TaskId, model.Note);
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
    public async Task<IActionResult> EditComment(EditCommentModel model)
    {
        if (!this.ModelState.IsValid || model == null)
        {
            return this.BadRequest("Wrong input");
        }

        var id = this.GetUserId();
        if (id == null)
        {
            return this.Unauthorized();
        }

        var result = await service.EditComment(id.Value, model.CommentId, model.Note);
        if (result.Status != ResultStatus.Success)
        {
            return this.BadRequest(result.Message);
        }

        return this.Ok();
    }
}
