using WebApi.Business.ListTasks;
using WebApi.Business.ToDoLists;
using WebApi.Mappers;
using WebApi.Services.Database;
using WebApi.Services.Database.Entities;

namespace WebApi.Services.TaskServices;

public class ListTaskService(ToDoListDbContext context) : IListTaskService
{
    public Task<Result> AddToDoListAsync(ToDoList? list)
    {
        throw new NotImplementedException();
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
