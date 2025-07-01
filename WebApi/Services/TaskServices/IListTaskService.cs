using WebApi.Business.ListTasks;

namespace WebApi.Services.TaskServices;

public interface IListTaskService
{
    Task<ListTaskInfo?> GetAllTasksAsync(long userId, long listId);

    Task<Result> AddToDoListAsync(ToDoList? list);

    Task<Result> DeleteToDoListAsync(long listId, long userId);

    Task<Result> UpdateToDoListAsync(ToDoList? list, long userId);

    Task<ToDoList?> GetToDoListAsync(long userId, long listId);
}
