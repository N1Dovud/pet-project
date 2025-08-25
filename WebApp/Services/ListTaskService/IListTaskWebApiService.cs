using WebApp.Business.Helpers;
using WebApp.Business.ListTasks;
using WebApp.Common;
using WebApp.Models.Helpers.Enums;

namespace WebApp.Services.ListTaskService;

public interface IListTaskWebApiService
{
    Task<ResultWithData<ListTaskInfo?>> GetListInfoAsync(long listId);

    Task<Result> EditTaskAsync(TaskDetails? task);

    Task<ResultWithData<TaskDetails?>> GetTaskDetailsAsync(long taskId);

    Task<Result> AddTaskAsync(TaskDetails? task, long? listId);

    Task<Result> DeleteTaskAsync(long taskId);

    Task<ResultWithData<List<TaskSummary?>?>> GetOverdueTasksAsync();

    Task<ResultWithData<List<TaskSummary?>?>> GetAssignedTasksAsync(StatusFilter filter, SortField? sortBy, bool descending);

    Task<Result> EditTaskStatusAsync(EditTaskStatus? model);

    Task<ResultWithData<List<TaskSummary?>?>> SearchTasksAsync<T>(SearchFields searchType, T queryValue);
}
