using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApi.Business;
using WebApi.Mappers;
using WebApi.Services.Database;
using WebApi.Services.Database.Entities;

namespace WebApi.Services.DatabaseService;

public class ToDoListDatabaseService(ToDoListDbContext context, ILogger<ToDoListDatabaseService> logger) : IToDoListDatabaseService
{
    public async Task<bool> AddToDoListAsync(ToDoList? list)
    {
        var entity = list?.ToEntity();
        if (entity != null)
        {
            _ = await context.ToDoLists.AddAsync(entity);
            _ = await context.SaveChangesAsync();
            return true;
        }
        else
        {
            logger.LogWarning("Failed to convert ToDoList to entity for saving.");
            return false;
        }
    }

    public async Task<DeleteResult> DeleteToDoListAsync(long listId, long ownerId)
    {
        ToDoListEntity? list = await context.ToDoLists.FindAsync(listId);
        if (list == null)
        {
            return DeleteResult.NotFound;
        }

        if (list.OwnerId != ownerId)
        {
            return DeleteResult.Forbidden;
        }

        _ = context.ToDoLists.Remove(list);
        _ = await context.SaveChangesAsync();
        return DeleteResult.Success;
    }

    public async Task<List<ToDoList>> GetAllToDoListsAsync(long userId)
    {
        var lists = await context.ToDoLists
            .Where(p => p.OwnerId == userId)
            .ToListAsync();
        return [.. lists.Select(l => l?.ToDomain())];
    }
}
