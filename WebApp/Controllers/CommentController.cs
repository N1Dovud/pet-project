using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Common;
using WebApp.Models.Helpers;
using WebApp.Services.CommentService;

namespace WebApp.Controllers;

[Controller]
[Authorize]
public class CommentController(ICommentWebApiService service) : Controller
{
    [HttpPost("add-comment")]
    public async Task<IActionResult> AddComment(AddCommentModel model)
    {
        if (!this.ModelState.IsValid)
        {
            return this.BadRequest("Wrong input");
        }

        var result = await service.AddComment(model.TaskId, model.Note);
        if (result.Status != ResultStatus.Success)
        {
            return this.BadRequest(result.Message);
        }

        return this.Redirect(model.ReturnUrl);
    }

    [HttpPost("delete-comment")]
    public async Task<IActionResult> DeleteComment(long commentId, string returnUrl)
    {
        if (!this.ModelState.IsValid)
        {
            return this.BadRequest("Wrong input");
        }

        var result = await service.DeleteComment(commentId);
        if (result.Status != ResultStatus.Success)
        {
            return this.BadRequest(result.Message);
        }

        return this.Redirect(returnUrl);
    }

    [HttpPost("edit-comment")]
    public async Task<IActionResult> EditComment(long commentId, string note, string returnUrl)
    {
        if (!this.ModelState.IsValid)
        {
            return this.BadRequest("Wrong input");
        }

        var result = await service.EditComment(commentId, note);
        if (result.Status != ResultStatus.Success)
        {
            return this.BadRequest(result.Message);
        }

        return this.Redirect(returnUrl);
    }
}
