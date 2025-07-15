using WebApi.Business.ListTasks;

namespace WebApi.Services.TaskServices;

public interface IListTaskService
{
    Task<ListTaskInfo?> GetAllTasksAsync(long userId, long listId);

    Task<Result> AddTaskAsync(TaskDetails task, long userId, long listId);

    Task<Result> DeleteTaskAsync(long taskId, long userId, long listId);

    Task<Result> UpdateTaskAsync(TaskDetails task, long userId, long listId);

    Task<TaskDetails> GetTaskAsync(long userId, long taskId);
}
