using Microsoft.EntityFrameworkCore;
using WebApi.Business.ListTasks;
using WebApi.Business.Tags;
using WebApi.Common;
using WebApi.Mappers;
using WebApi.Services.Database;
using WebApi.Services.Database.Entities;

namespace WebApi.Services.TagServices;

internal class TagService(ToDoListDbContext context): ITagService
{
    public async Task<Result> AddTag(long userId, string tagName, long taskId)
    {
        if (string.IsNullOrEmpty(tagName))
        {
            return Result.Error("tagName cannot be null or empty");
        }

        tagName = tagName.Trim().ToLowerInvariant();
        var task = await context.Tasks
                    .Include(t => t.ToDoList)
                    .Include(t => t.Tags)
                    .SingleOrDefaultAsync(t => t.Id == taskId);
        if (task == null)
        {
            return Result.NotFound("No task found");
        }

        if (task.ToDoList?.OwnerId != userId)
        {
            return Result.Forbidden("You can't touch this task");
        }

        if (task.Tags.Any(tag => tag.Name == tagName))
        {
            return Result.Error("this tag already exists!");
        }

        var tag = await context.Tags
                            .FirstOrDefaultAsync(t => t.Name == tagName);
        if (tag == null)
        {
            tag = new TagEntity
            {
                Name = tagName,
            };
            _ = context.Tags.Add(tag);
        }

        task.Tags.Add(tag);

        var rowCount = await context.SaveChangesAsync();

        if (rowCount == 0)
        {
            return Result.Error("db problem");
        }

        return Result.Success();
    }

    public async Task<Result> DeleteTag(long userId, long tagId, long taskId)
    {
        var task = await context.Tasks
            .Include(t => t.Tags)
            .Where(t => t.Id == taskId && t.ToDoList != null && t.ToDoList.OwnerId == userId)
            .SingleOrDefaultAsync();
        if (task == null)
        {
            return Result.NotFound("Task not found or not yours!");
        }

        var tag = await context.Tags.FindAsync(tagId);

        if (tag == null)
        {
            return Result.NotFound("No such tag!");
        }

        if (!task.Tags.Any(tag => tag.Id == tagId))
        {
            return Result.NotFound("task does not have such a tag");
        }

        _ = task.Tags.Remove(tag);

        var rowCount = await context.SaveChangesAsync();
        if (rowCount == 0)
        {
            return Result.Error("failed to remove a tag");
        }

        return Result.Success();
    }

    public async Task<ResultWithData<List<Tag?>?>> GetAllTags(long userId)
    {
        var tags = await context.Tags
            .Where(t => t.Tasks.Any(task => task.ToDoList != null && task.ToDoList.OwnerId == userId))
            .ToListAsync();
        return ResultWithData<List<Tag?>?>.Success([.. tags.Select(t => t.ToDomain())]);
    }

    public async Task<ResultWithData<List<TaskSummary?>?>> GetTasksByTag(long tagId, long userId)
    {
        var tasks = await context.Tasks
            .Include(t => t.Tags)
            .Where(t => t.ToDoList != null && t.ToDoList.OwnerId == userId)
            .Where(t => t.Tags.Any(tag => tag.Id == tagId))
            .Select(t => t)
            .ToListAsync();

        return ResultWithData<List<TaskSummary?>?>.Success([.. tasks.Select(t => t.ToTaskSummary())]);
    }
}
