using Microsoft.EntityFrameworkCore;
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
}
