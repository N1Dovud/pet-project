using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Common;
using WebApp.Helpers;
using WebApp.Models.Helpers;
using WebApp.Services.CommentService;

namespace WebApp.Controllers;

[Controller]
[Authorize]
internal class CommentController(ICommentWebApiService service): Controller
{
    [HttpPost("add-comment")]
    public async Task<IActionResult> AddComment(AddCommentModel model)
    {
        if (!this.ModelState.IsValid || model.Note == null || model == null)
        {
            return this.BadRequest("Wrong input");
        }

        var result = await service.AddComment(model.TaskId, model.Note);
        if (result.Status != ResultStatus.Success)
        {
            return this.ToHttpResponse(result);
        }

        return this.RedirectToAction("GetTaskDetails", "ListTask", new
        {
            taskId = model.TaskId,
            returnUrl = model.ReturnUrl,
            isOwner = true,
        });
    }

    [HttpPost("delete-comment")]
    public async Task<IActionResult> DeleteComment(long commentId, string returnUrl, long taskId)
    {
        if (!this.ModelState.IsValid)
        {
            return this.BadRequest("Wrong input");
        }

        var result = await service.DeleteComment(commentId);
        if (result.Status != ResultStatus.Success)
        {
            return this.ToHttpResponse(result);
        }

        return this.RedirectToAction("GetTaskDetails", "ListTask", new
        {
            taskId = taskId,
            returnUrl = returnUrl,
            isOwner = true,
        });
    }

    [HttpPost("edit-comment")]
    public async Task<IActionResult> EditComment(long commentId, string note, string returnUrl, long taskId)
    {
        if (!this.ModelState.IsValid)
        {
            return this.BadRequest("Wrong input");
        }

        var result = await service.EditComment(commentId, note);
        if (result.Status != ResultStatus.Success)
        {
            return this.ToHttpResponse(result);
        }

        return this.RedirectToAction("GetTaskDetails", "ListTask", new
        {
            taskId = taskId,
            returnUrl = returnUrl,
            isOwner = true,
        });
    }
}
