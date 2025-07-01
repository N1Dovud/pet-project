using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApi.Business;
using WebApi.Mappers;
using WebApi.Services.Database;
using WebApi.Services.Database.Entities;

namespace WebApi.Services.DatabaseService;

public class ToDoListDatabaseService(ToDoListDbContext context, ILogger<ToDoListDatabaseService> logger) : IToDoListDatabaseService
{
    public async Task<Result> AddToDoListAsync(ToDoList? list)
    {
        var entity = list?.ToEntity();
        if (entity != null)
        {
            _ = await context.ToDoLists.AddAsync(entity);
            _ = await context.SaveChangesAsync();
            return Result.Success("list created");
        }
        else
        {
            logger.LogWarning("Failed to convert ToDoList to entity for saving.");
            return Result.Error();
        }
    }

    public async Task<Result> DeleteToDoListAsync(long listId, long userId)
    {
        ToDoListEntity? list = await context.ToDoLists.FindAsync(listId);
        if (list == null)
        {
            return Result.NotFound();
        }

        if (list.OwnerId != userId)
        {
            return Result.Forbidden("not the owner of the list");
        }

        _ = context.ToDoLists.Remove(list);
        _ = await context.SaveChangesAsync();
        return Result.Success("successfully deleted");
    }

    public async Task<List<ToDoList>> GetAllToDoListsAsync(long userId)
    {
        var lists = await context.ToDoLists
            .Where(p => p.OwnerId == userId)
            .ToListAsync();
        return [.. lists.Select(l => l?.ToDomain())];
    }

    public async Task<ToDoList?> GetToDoListAsync(long userId, long listId)
    {
        var list = await context.ToDoLists
            .FirstOrDefaultAsync(p => p.Id == listId);

        if (list == null || list.OwnerId != userId)
        {
            return null;
        }

        return list.ToDomain();
    }

    public async Task<Result> UpdateToDoListAsync(ToDoList? list, long userId)
    {
        if (list == null)
        {
            return Result.NotFound();
        }

        ToDoListEntity? entity = await context.ToDoLists.FindAsync(list.Id);
        if (entity == null)
        {
            return Result.NotFound("list not found");
        }

        if (entity.OwnerId != userId)
        {
            return Result.Forbidden("not the owner of the list");
        }

        entity.Description = list.Description;
        entity.Title = list.Title;

        _ = await context.SaveChangesAsync();
        return Result.Success("successfully updated");
    }
}
