using WebApi.Common;

namespace WebApi.Services.CommentServices;

internal interface ICommentService
{
    Task<Result> AddComment(long userId, long taskId, string note);

    Task<Result> DeleteComment(long userId, long commentId);

    Task<Result> EditComment(long userId, long commentId, string note);
}
