using Microsoft.EntityFrameworkCore;
using WebApi.Business.ListTasks;
using WebApi.Business.Tags;
using WebApi.Common;
using WebApi.Mappers;
using WebApi.Services.Database;

namespace WebApi.Services.TagsServices;

public class TagService(ToDoListDbContext context) : ITagService
{
    public async Task<ResultWithData<List<Tag?>?>> GetAllTags(long userId)
    {
        var tags = await context.Tasks
            .Include(t => t.Tags)
            .Include(t => t.ToDoList)
            .Where(t => t.ToDoList != null && t.ToDoList.OwnerId == userId)
            .SelectMany(t => t.Tags)
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
