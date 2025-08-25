using WebApi.Business.Helpers;
using WebApi.Business.ListTasks;
using WebApi.Common;
using WebApi.Models.Helpers;

namespace WebApi.Services.TaskServices;

public interface IListTaskService
{
    Task<ResultWithData<ListTaskInfo?>> GetAllTasksAsync(long userId, long listId);

    Task<Result> AddTaskAsync(TaskDetails task, long userId, long listId);

    Task<Result> DeleteTaskAsync(long taskId, long userId);

    Task<Result> UpdateTaskAsync(TaskDetails task, long userId);

    Task<ResultWithData<TaskDetails?>> GetTaskAsync(long userId, long taskId);

    Task<ResultWithData<List<TaskSummary?>?>> GetOverdueTasks(long userId);

    Task<ResultWithData<List<TaskSummary?>?>> GetAssignedTasks(long userId, StatusFilter filter, SortField? sortBy, bool descending);

    Task<Result> EditTaskStatusAsync(long userId, EditTaskStatus model);

    Task<ResultWithData<List<TaskSummary?>?>> SearchTasksAsync(long userId, SearchFields searchType, DateTime queryValue);

    Task<ResultWithData<List<TaskSummary?>?>> SearchTasksAsync(long userId, SearchFields searchType, string queryValue);
}
