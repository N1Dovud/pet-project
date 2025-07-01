using Microsoft.EntityFrameworkCore;
using WebApi.Business.ListTasks;
using WebApi.Business.ToDoLists;
using WebApi.Mappers;
using WebApi.Services.Database;
using WebApi.Services.Database.Entities;

namespace WebApi.Services.TaskServices;

public class ListTaskService(ToDoListDbContext context) : IListTaskService
{
    public async Task<Result> AddTaskAsync(TaskDetails task, long userId, long listId)
    {
        var list = await context.ToDoLists
                .Include(l => l.Tasks)
                .FirstOrDefaultAsync(l => l.Id == listId);
        if (list == null)
        {
            return Result.NotFound("list not found");
        }

        if (list.OwnerId != userId)
        {
            return Result.Forbidden("You do not own this list.");
        }

        var entity = task.ToEntity(listId);
        list.Tasks.Add(entity);

        int rows = await context.SaveChangesAsync();
        if (rows == 0)
        {
            return Result.Error("Nothing added");
        }

        return Result.Success("Task added successfully.");
    }

    public Task<Result> DeleteToDoListAsync(long listId, long userId)
    {
        throw new NotImplementedException();
    }

    public async Task<ListTaskInfo?> GetAllTasksAsync(long userId, long listId)
    {
        ToDoListEntity? list = await context.ToDoLists
            .FindAsync(listId);
        if (list == null)
        {
            return null;
        }

        if (list.OwnerId != userId)
        {
            return null;
        }

        return list.ToListTask();
    }

    public Task<ToDoList?> GetToDoListAsync(long userId, long listId)
    {
        throw new NotImplementedException();
    }

    public Task<Result> UpdateToDoListAsync(ToDoList? list, long userId)
    {
        throw new NotImplementedException();
    }
}
