using System.Text.Json;
using WebApp.Business;
using WebApp.Mappers;
using WebApp.Models;

namespace WebApp.Services.DatabaseService;

public class ToDoListWebApiService : IToDoListWebApiService
{
    private readonly HttpClient httpClient;
    private readonly string baseUrl;

    public ToDoListWebApiService(IHttpClientFactory factory, IConfiguration configuration)
    {
        this.httpClient = factory.CreateClient("ApiWithJwt");
        this.baseUrl = configuration["BaseUrl"];
    }

    public async Task<List<ToDoList?>?> GetToDoLists()
    {
        var route = "getlists";
        var uri = new Uri(this.baseUrl + route);
        var response = await this.httpClient.GetAsync(uri);
        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        var json = await response.Content.ReadAsStringAsync();
        List<ToDoListWebApiModel>? lists = JsonSerializer.Deserialize<List<ToDoListWebApiModel>>(json);
        if (lists == null)
        {
            return null;
        }

        return [.. lists.Select(l => l.ToDomain())];

    }
}
