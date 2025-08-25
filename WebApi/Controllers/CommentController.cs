using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Helpers;
using WebApi.Models.Helpers;
using WebApi.Services.CommentServices;

namespace WebApi.Controllers;

[ApiController]
[Authorize]
[Route("api")]
public class CommentController(ICommentService service): ControllerBase
{
    [HttpPost("add-comment")]
    public async Task<IActionResult> AddComment(AddCommentModel model)
    {
        if (!this.ModelState.IsValid || model == null || model.Note == null)
        {
            return this.BadRequest("Wrong input");
        }

        var id = this.GetUserId();
        if (id == null)
        {
            return this.Unauthorized();
        }

        var result = await service.AddComment(id.Value, model.TaskId, model.Note);
        return this.ToHttpResponse(result);
    }

    [HttpDelete("delete-comment")]
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
        return this.ToHttpResponse(result);
    }

    [HttpPut("edit-comment")]
    public async Task<IActionResult> EditComment(EditCommentModel model)
    {
        if (!this.ModelState.IsValid || model == null || model.Note == null)
        {
            return this.BadRequest("Wrong input");
        }

        var id = this.GetUserId();
        if (id == null)
        {
            return this.Unauthorized();
        }

        var result = await service.EditComment(id.Value, model.CommentId, model.Note);
        return this.ToHttpResponse(result);
    }
}
