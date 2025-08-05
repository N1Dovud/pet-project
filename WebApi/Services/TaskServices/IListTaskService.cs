using WebApi.Business.Helpers;
using WebApi.Business.ListTasks;

namespace WebApi.Services.TaskServices;

public interface IListTaskService
{
    Task<ListTaskInfo?> GetAllTasksAsync(long userId, long listId);

    Task<Result> AddTaskAsync(TaskDetails task, long userId, long listId);

    Task<Result> DeleteTaskAsync(long taskId, long userId);

    Task<Result> UpdateTaskAsync(TaskDetails task, long userId);

    Task<TaskDetails?> GetTaskAsync(long userId, long taskId);

    Task<List<TaskSummary?>?> GetOverdueTasks(long userId);

    Task<List<TaskSummary?>?> GetAssignedTasks(long userId);

    Task<Result> EditTaskStatusAsync(long userId, EditTaskStatus model);
}
