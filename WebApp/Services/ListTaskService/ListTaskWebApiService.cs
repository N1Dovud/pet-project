using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using WebApp.Business.Helpers;
using WebApp.Business.ListTasks;
using WebApp.Common;
using WebApp.Mappers;
using WebApp.Models.Helpers.Enums;
using WebApp.Models.ListTasks;
using WebApp.Models.ToDoLists;

namespace WebApp.Services.ListTaskService;

public class ListTaskWebApiService : IListTaskWebApiService
{
    private readonly HttpClient httpClient;
    private readonly string baseUrl;
    private readonly JsonSerializerOptions options = new JsonSerializerOptions
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    };

    public ListTaskWebApiService(IHttpClientFactory factory, IConfiguration configuration)
    {
        this.httpClient = factory.CreateClient("ApiWithJwt");
        this.baseUrl = configuration["WebApiAddress"];
    }

    public async Task<Result> AddTaskAsync(TaskDetails? task, long listId)
    {
        if (task == null)
        {
            return Result.Error("Task cannot be null");
        }

        var route = "task";
        var uri = new Uri(this.baseUrl + route + "?listId=" + listId);
        var response = await this.httpClient.PostAsJsonAsync(uri, task.ToWebApiModel(), this.options);

        if (response.StatusCode == HttpStatusCode.Created || response.StatusCode == HttpStatusCode.OK)
        {
            return Result.Success("Task added successfully");
        }

        return response.StatusCode switch
        {
            HttpStatusCode.BadRequest => Result.Error("Bad request"),
            HttpStatusCode.Unauthorized => Result.Unauthorized("Unauthorized"),
            _ => Result.Error($"Failed to add task. Status code: {response.StatusCode}"),
        };
    }

    public async Task<Result> DeleteTaskAsync(long taskId)
    {
        var route = "task";
        var uri = new Uri(this.baseUrl + route + "?taskId=" + taskId);
        var response = await this.httpClient.DeleteAsync(uri);
        if (response.StatusCode == HttpStatusCode.OK)
        {
            return Result.Success("Task deleted successfully");
        }

        return response.StatusCode switch
        {
            HttpStatusCode.BadRequest => Result.Error("Bad request"),
            HttpStatusCode.Unauthorized => Result.Unauthorized("Unauthorized"),
            _ => Result.Error($"Failed to delete a task. Status code: {response.StatusCode}"),
        };
    }

    public async Task<Result> EditTaskAsync(TaskDetails? task)
    {
        if (task == null)
        {
            return Result.Error("Task cannot be null");
        }

        var route = "task";
        var uri = new Uri(this.baseUrl + route);
        var response = await this.httpClient.PutAsJsonAsync(uri, task.ToWebApiModel(), this.options);
        if (response.StatusCode == HttpStatusCode.OK)
        {
            return Result.Success("Task updated successfully");
        }

        return response.StatusCode switch
        {
            HttpStatusCode.BadRequest => Result.Error("Bad request"),
            HttpStatusCode.Unauthorized => Result.Unauthorized("Unauthorized"),
            _ => Result.Error($"Failed to update a task. Status code: {response.StatusCode}"),
        };
    }

    public async Task<ListTaskInfo?> GetListInfoAsync(long listId)
    {
        var route = "tasks";
        var uri = new Uri(this.baseUrl + route + "?listId=" + listId);
        var response = await this.httpClient.GetAsync(uri);
        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        var json = await response.Content.ReadAsStringAsync();
        var listTask = JsonSerializer.Deserialize<ListTaskInfoWebApiModel>(json, this.options);

        return listTask.ToDomain();
    }

    public async Task<TaskDetails?> GetTaskDetailsAsync(long taskId)
    {
        var route = "task";
        var uri = new Uri(this.baseUrl + route + "?taskId=" + taskId);
        var response = await this.httpClient.GetAsync(uri);
        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        var json = await response.Content.ReadAsStringAsync();
        var taskDetails = JsonSerializer.Deserialize<TaskDetailsWebApiModel>(json, this.options);
        return taskDetails?.ToDomain();
    }

    public async Task<List<TaskSummary?>?> GetOverdueTasksAsync()
    {
        var route = "overdue";
        var uri = new Uri(this.baseUrl + route);
        var response = await this.httpClient.GetAsync(uri);
        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        var json = await response.Content.ReadAsStringAsync();
        var tasks = JsonSerializer.Deserialize<List<TaskSummaryWebApiModel>>(json, this.options);
        return [.. tasks.Select(t => t.ToDomain())];
    }

    public async Task<List<TaskSummary?>?> GetAssignedTasksAsync(StatusFilter filter, SortField? sortBy, bool descending)
    {
        var route = "assigned";
        var uri = new Uri($"{this.baseUrl}{route}?filter={filter}&sortBy={sortBy}&descending={descending}");
        var response = await this.httpClient.GetAsync(uri);
        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        var json = await response.Content.ReadAsStringAsync();
        var tasks = JsonSerializer.Deserialize<List<TaskSummaryWebApiModel>>(json, this.options);
        return [.. tasks.Select(t => t.ToDomain())];
    }

    public async Task<Result> EditTaskStatusAsync(EditTaskStatus? model)
    {
        var route = "status-update";
        var uri = new Uri(this.baseUrl + route);
        var response = await this.httpClient.PostAsJsonAsync(uri, model?.ToApiModel());
        if (response.StatusCode == HttpStatusCode.OK)
        {
            return Result.Success("Task updated successfully");
        }

        return response.StatusCode switch
        {
            HttpStatusCode.BadRequest => Result.Error("Bad request"),
            HttpStatusCode.Unauthorized => Result.Unauthorized("Unauthorized"),
            _ => Result.Error($"Failed to update a task status. Status code: {response.StatusCode}"),
        };
    }

    public async Task<ResultWithData<List<TaskSummary?>?>> SearchTasksAsync<T>(SearchFields searchType, T queryValue)
    {
        var route = "task-search";
        var uri = new Uri($"{this.baseUrl}{route}?searchType={searchType}&queryValue={queryValue}");

        var response = await this.httpClient.GetAsync(uri);
        if (!response.IsSuccessStatusCode)
        {
            return ResultWithData<List<TaskSummary?>?>.Error($"Failed to search tasks. Status code: {response.StatusCode}");
        }

        var json = await response.Content.ReadAsStringAsync();
        var tasks = JsonSerializer.Deserialize<List<TaskSummaryWebApiModel>>(json, this.options);
        if (tasks == null)
        {
            return ResultWithData<List<TaskSummary?>?>.Error("No tasks found");
        }

        return ResultWithData<List<TaskSummary?>?>.Success([.. tasks.Select(t => t.ToDomain())], "Tasks found successfully");
    }
}
