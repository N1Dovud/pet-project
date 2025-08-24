using System.Net;
using System.Text.Json;
using WebApp.Business.Helpers;
using WebApp.Business.ListTasks;
using WebApp.Common;
using WebApp.Helpers;
using WebApp.Mappers;
using WebApp.Models.Helpers.Enums;
using WebApp.Models.ListTasks;

namespace WebApp.Services.ListTaskService;

internal class ListTaskWebApiService : IListTaskWebApiService
{
    private readonly HttpClient httpClient;
    private readonly string? baseUrl;
    private readonly JsonSerializerOptions options = new JsonSerializerOptions
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    };

    public ListTaskWebApiService(IHttpClientFactory factory, IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(factory);
        this.httpClient = factory.CreateClient("ApiWithJwt");
        this.baseUrl = configuration["WebApiAddress"];
    }

    public async Task<Result> AddTaskAsync(TaskDetails? task, long? listId)
    {
        if (task == null)
        {
            return Result.Error("Task cannot be null");
        }

        var route = "task";
        var uri = new Uri(this.baseUrl + route + "?listId=" + listId);
        var response = await this.httpClient.PostAsJsonAsync(uri, task.ToWebApiModel(), this.options);
        return await HttpResponseMapper.MapHttpResponseToResult(response);
    }

    public async Task<Result> DeleteTaskAsync(long taskId)
    {
        var route = "task";
        var uri = new Uri(this.baseUrl + route + "?taskId=" + taskId);
        var response = await this.httpClient.DeleteAsync(uri);
        return await HttpResponseMapper.MapHttpResponseToResult(response);
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
        return await HttpResponseMapper.MapHttpResponseToResult(response);
    }

    public async Task<ResultWithData<ListTaskInfo?>> GetListInfoAsync(long listId)
    {
        var route = "tasks";
        var uri = new Uri(this.baseUrl + route + "?listId=" + listId);
        var response = await this.httpClient.GetAsync(uri);
        if (!response.IsSuccessStatusCode)
        {
            return await HttpResponseMapper.MapHttpResponseToResult<ListTaskInfo?>(response);
        }

        var json = await response.Content.ReadAsStringAsync();
        var listTask = JsonSerializer.Deserialize<ListTaskInfoWebApiModel>(json, this.options);

        return ResultWithData<ListTaskInfo?>.Success(listTask.ToDomain());
    }

    public async Task<ResultWithData<TaskDetails?>> GetTaskDetailsAsync(long taskId)
    {
        var route = "task";
        var uri = new Uri(this.baseUrl + route + "?taskId=" + taskId);
        var response = await this.httpClient.GetAsync(uri);
        if (!response.IsSuccessStatusCode)
        {
            return await HttpResponseMapper.MapHttpResponseToResult<TaskDetails?>(response);
        }

        var json = await response.Content.ReadAsStringAsync();
        var taskDetails = JsonSerializer.Deserialize<TaskDetailsWebApiModel>(json, this.options);
        return ResultWithData<TaskDetails?>.Success(taskDetails?.ToDomain());
    }

    public async Task<ResultWithData<List<TaskSummary?>?>> GetOverdueTasksAsync()
    {
        var route = "overdue";
        var uri = new Uri(this.baseUrl + route);
        var response = await this.httpClient.GetAsync(uri);
        if (!response.IsSuccessStatusCode)
        {
            return await HttpResponseMapper.MapHttpResponseToResult<List<TaskSummary?>?>(response);
        }

        var json = await response.Content.ReadAsStringAsync();
        var tasks = JsonSerializer.Deserialize<List<TaskSummaryWebApiModel>>(json, this.options);
        return ResultWithData<List<TaskSummary?>?>.Success([.. tasks?.Select(t => t.ToDomain()) ?? []]);
    }

    public async Task<ResultWithData<List<TaskSummary?>?>> GetAssignedTasksAsync(StatusFilter filter, SortField? sortBy, bool descending)
    {
        var route = "assigned";
        var uri = new Uri($"{this.baseUrl}{route}?filter={filter}&sortBy={sortBy}&descending={descending}");
        var response = await this.httpClient.GetAsync(uri);
        if (!response.IsSuccessStatusCode)
        {
            return await HttpResponseMapper.MapHttpResponseToResult<List<TaskSummary?>?>(response);
        }

        var json = await response.Content.ReadAsStringAsync();
        var tasks = JsonSerializer.Deserialize<List<TaskSummaryWebApiModel>>(json, this.options);
        return ResultWithData<List<TaskSummary?>?>.Success([.. tasks?.Select(t => t.ToDomain()) ?? []]);
    }

    public async Task<Result> EditTaskStatusAsync(EditTaskStatus? model)
    {
        var route = "status-update";
        var uri = new Uri(this.baseUrl + route);
        var response = await this.httpClient.PutAsJsonAsync(uri, model?.ToApiModel());
        return await HttpResponseMapper.MapHttpResponseToResult(response);
    }

    public async Task<ResultWithData<List<TaskSummary?>?>> SearchTasksAsync<T>(SearchFields searchType, T queryValue)
    {
        var route = "task-search";
        var uri = new Uri($"{this.baseUrl}{route}?searchType={searchType}&queryValue={queryValue}");

        var response = await this.httpClient.GetAsync(uri);
        if (!response.IsSuccessStatusCode)
        {
            return await HttpResponseMapper.MapHttpResponseToResult<List<TaskSummary?>?>(response);
        }

        var json = await response.Content.ReadAsStringAsync();
        var tasks = JsonSerializer.Deserialize<List<TaskSummaryWebApiModel>>(json, this.options);
        return ResultWithData<List<TaskSummary?>?>.Success([.. tasks?.Select(t => t.ToDomain()) ?? []]);
    }
}
