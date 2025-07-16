using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using WebApp.Business.ListTasks;
using WebApp.Mappers;
using WebApp.Models.ListTasks;
using WebApp.Models.ToDoLists;

namespace WebApp.Services.DatabaseService;

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

    public async Task<Result> EditTaskAsync(long taskId, TaskDetails task)
    {
        if (task == null)
        {
            return Result.Error("List cannot be null");
        }

        var route = "task";
        var uri = new Uri(this.baseUrl + route + "?taskId=" + taskId);
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
        ListTaskInfoWebApiModel? listTask = JsonSerializer.Deserialize<ListTaskInfoWebApiModel>(json, this.options);

        return listTask.ToDomain();
    }
}
