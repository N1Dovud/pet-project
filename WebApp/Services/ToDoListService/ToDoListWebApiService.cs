using System.Net;
using System.Text.Json;
using WebApp.Business.ListTasks;
using WebApp.Business.ToDoLists;
using WebApp.Common;
using WebApp.Helpers;
using WebApp.Mappers;
using WebApp.Models.ToDoLists;

namespace WebApp.Services.ToDoListService;

public class ToDoListWebApiService : IToDoListWebApiService
{
    private readonly HttpClient httpClient;
    private readonly string? baseUrl;
    private readonly JsonSerializerOptions options = new JsonSerializerOptions
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    };

    public ToDoListWebApiService(IHttpClientFactory factory, IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(factory);
        ArgumentNullException.ThrowIfNull(configuration);
        this.httpClient = factory.CreateClient("ApiWithJwt");
        this.baseUrl = configuration["WebApiAddress"];
    }

    public async Task<Result> AddToDoListAsync(ToDoList? list)
    {
        if (list == null)
        {
            return Result.Error("List cannot be null");
        }

        var route = "list";
        var uri = new Uri(this.baseUrl + route);
        var response = await this.httpClient.PostAsJsonAsync(uri, list.ToWebApiModel(), this.options);
        return await HttpResponseMapper.MapHttpResponseToResult(response);
    }

    public async Task<Result> UpdateToDoListAsync(ToDoList? list)
    {
        if (list == null)
        {
            return Result.Error("List cannot be null");
        }

        var route = "list";
        var uri = new Uri(this.baseUrl + route);
        var response = await this.httpClient.PutAsJsonAsync(uri, list.ToWebApiModel(), this.options);
        return await HttpResponseMapper.MapHttpResponseToResult(response);
    }

    public async Task<Result> DeleteToDoListAsync(long listId)
    {
        var route = "list";
        var uri = new Uri($"{this.baseUrl}{route}?listId={listId}");
        var response = await this.httpClient.DeleteAsync(uri);

        return await HttpResponseMapper.MapHttpResponseToResult(response);
    }

    public async Task<ResultWithData<List<ToDoList?>?>> GetToDoListsAsync()
    {
        var route = "lists";
        var uri = new Uri(this.baseUrl + route);
        var response = await this.httpClient.GetAsync(uri);
        if (!response.IsSuccessStatusCode)
        {
            return await HttpResponseMapper.MapHttpResponseToResult<List<ToDoList?>?>(response);
        }

        var json = await response.Content.ReadAsStringAsync();
        var lists = JsonSerializer.Deserialize<List<ToDoListWebApiModel>>(json, this.options);
        return ResultWithData<List<ToDoList?>?>.Success([.. lists?.Select(l => l?.ToDomain()) ?? []]);
    }

    public async Task<ResultWithData<ToDoList?>> GetToDoListAsync(long listId)
    {
        var route = "list";
        var uri = new Uri($"{this.baseUrl}{route}?listId={listId}");
        var response = await this.httpClient.GetAsync(uri);
        if (!response.IsSuccessStatusCode)
        {
            return await HttpResponseMapper.MapHttpResponseToResult<ToDoList?>(response);
        }

        var json = await response.Content.ReadAsStringAsync();
        var list = JsonSerializer.Deserialize<ToDoListWebApiModel>(json, this.options);
        return ResultWithData<ToDoList?>.Success(list?.ToDomain());
    }
}
