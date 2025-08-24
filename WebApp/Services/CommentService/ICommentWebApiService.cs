using WebApp.Common;

namespace WebApp.Services.CommentService;

internal interface ICommentWebApiService
{
    Task<Result> AddComment(long taskId, string note);

    Task<Result> DeleteComment(long commentId);

    Task<Result> EditComment(long commentId, string note);
}
