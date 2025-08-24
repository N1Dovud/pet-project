using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WebApi.Business.ToDoLists;
using WebApi.Common;
using WebApi.Mappers;
using WebApi.Services.Database;

namespace WebApi.Services.ListServices;

internal class ToDoListService(ToDoListDbContext context, ILogger<ToDoListService> logger): IToDoListService
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
            return Result.Error("could resolve the list");
        }
    }

    public async Task<Result> DeleteToDoListAsync(long listId, long userId)
    {
        var list = await context.ToDoLists.FindAsync(listId);
        if (list == null)
        {
            return Result.NotFound("list not found");
        }

        if (list.OwnerId != userId)
        {
            return Result.Forbidden("not the owner of the list");
        }

        _ = context.ToDoLists.Remove(list);
        _ = await context.SaveChangesAsync();
        return Result.Success("successfully deleted");
    }

    public async Task<ResultWithData<List<ToDoList?>?>> GetAllToDoListsAsync(long userId)
    {
        try
        {
            if (userId <= 0)
            {
                return ResultWithData<List<ToDoList?>?>.Error("Invalid user ID");
            }

            var lists = await context.ToDoLists
                .Where(p => p.OwnerId == userId)
                .ToListAsync();

            var domainLists = lists.Select(l => l.ToDomain()).ToList();
            return ResultWithData<List<ToDoList?>?>.Success(domainLists);
        }
        catch (SqlException ex)
        {
            // Database connectivity issues
            // Log the exception
            return ResultWithData<List<ToDoList?>?>.Error("Database connection failed");
        }
        catch (InvalidOperationException ex)
        {
            // EF context issues (disposed context, etc.)
            // Log the exception
            return ResultWithData<List<ToDoList?>?>.Error("Database operation failed");
        }
        catch (TaskCanceledException ex)
        {
            // Query timeout or cancellation
            // Log the exception
            return ResultWithData<List<ToDoList?>?>.Error("Operation timed out");
        }
    }

    public async Task<ResultWithData<ToDoList?>> GetToDoListAsync(long userId, long listId)
    {
        var list = await context.ToDoLists
            .FirstOrDefaultAsync(p => p.Id == listId);

        if (list == null)
        {
            return ResultWithData<ToDoList?>.NotFound("lists not found");
        }

        if (list.OwnerId != userId)
        {
            return ResultWithData<ToDoList?>.Forbidden("not the owner!");
        }

        return ResultWithData<ToDoList?>.Success(list.ToDomain());
    }

    public async Task<Result> UpdateToDoListAsync(ToDoList? list, long userId)
    {
        if (list == null)
        {
            return Result.NotFound();
        }

        var entity = await context.ToDoLists.FindAsync(list.Id);
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
