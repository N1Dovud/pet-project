using Microsoft.EntityFrameworkCore;
using WebApi.Business;
using WebApi.Mappers;
using WebApi.Services.Database;

namespace WebApi.Services.DatabaseService;

public class ToDoListDatabaseService(ToDoListDbContext context) : IToDoListDatabaseService
{
    public async Task<List<ToDoList>> GetAllToDoListsAsync(long? userId)
    {
        var lists = await context.ListPermissions
            .Where(p => p.UserId == userId)
            .Select(p => p.ToDoList)
            .Distinct()
            .ToListAsync();
        return [.. lists.Select(l => l?.ToDomain())];
    }
}
