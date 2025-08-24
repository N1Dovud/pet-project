using Microsoft.EntityFrameworkCore;
using WebApi.Common;
using WebApi.Services.Database;
using WebApi.Services.Database.Entities;

namespace WebApi.Services.CommentServices;

internal class CommentService(ToDoListDbContext context): ICommentService
{
    public async Task<Result> AddComment(long userId, long taskId, string note)
    {
        var task = await context.Tasks
                    .Include(t => t.ToDoList)
                    .FirstOrDefaultAsync(t => t.Id == taskId);

        if (task == null)
        {
            return Result.NotFound("task not found");
        }

        if (task.ToDoList?.OwnerId != userId)
        {
            return Result.Forbidden("you are not the owner of the task");
        }

        var comment = new CommentEntity
        {
            Note = note,
            UserId = userId,
        };

        task.Comments.Add(comment);

        _ = await context.SaveChangesAsync();

        return Result.Success();
    }

    public async Task<Result> DeleteComment(long userId, long commentId)
    {
        var comment = await context.Comments
                        .Include(c => c.Task)
                            .ThenInclude(t => t == null ? null : t.ToDoList)
                        .FirstOrDefaultAsync(c => c.Id == commentId);

        if (comment == null)
        {
            return Result.NotFound("comment not found");
        }

        if (comment.Task?.ToDoList?.OwnerId != userId)
        {
            return Result.Forbidden("you are not the owner of the task");
        }

        _ = context.Comments.Remove(comment);
        _ = await context.SaveChangesAsync();
        return Result.Success();
    }

    public async Task<Result> EditComment(long userId, long commentId, string note)
    {
        var comment = await context.Comments
                        .Include(c => c.Task)
                            .ThenInclude(t => t == null ? null : t.ToDoList)
                        .FirstOrDefaultAsync(c => c.Id == commentId);

        if (comment == null)
        {
            return Result.NotFound("comment not found");
        }

        if (comment.Task?.ToDoList?.OwnerId != userId)
        {
            return Result.Forbidden("you are not the owner of the task");
        }

        comment.Note = note;
        comment.LastEditDateTime = DateTime.UtcNow;

        _ = await context.SaveChangesAsync();
        return Result.Success();
    }
}
