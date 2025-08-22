using WebApp.Common;

namespace WebApp.Services.CommentService;

public class CommentWebApiService : ICommentWebApiService
{
    public Task<Result> AddComment(long taskId, string note)
    {
        throw new NotImplementedException();
    }

    public Task<Result> DeleteComment(long commentId)
    {
        throw new NotImplementedException();
    }

    public Task<Result> EditComment(long commentId, string note)
    {
        throw new NotImplementedException();
    }
}
