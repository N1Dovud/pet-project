using WebApp.Business.Helpers;
using WebApp.Business.ListTasks;
using WebApp.Models.Helpers;
using WebApp.Models.ListTasks;

namespace WebApp.Services.DatabaseService;

public interface IListTaskWebApiService
{
    Task<ListTaskInfo?> GetListInfoAsync(long listId);

    Task<Result> EditTaskAsync(TaskDetails? task);

    Task<TaskDetails?> GetTaskDetailsAsync(long taskId);

    Task<Result> AddTaskAsync(TaskDetails? task, long listId);

    Task<Result> DeleteTaskAsync(long taskId);

    Task<List<TaskSummary?>?> GetOverdueTasksAsync();

    Task<List<TaskSummary?>?> GetAssignedTasksAsync(StatusFilter filter, SortField? sortBy, bool descending);

    Task<Result> EditTaskStatusAsync(EditTaskStatus? model);
}
