using System.Text.Json;
using WebApp.Business.ListTasks;
using WebApp.Business.Tags;
using WebApp.Common;
using WebApp.Mappers;
using WebApp.Models.ListTasks;
using WebApp.Models.Tags;

namespace WebApp.Services.TagService;

public class TagWebApiService : ITagWebApiService
{
    private readonly HttpClient httpClient;
    private readonly string baseUrl;
    private readonly JsonSerializerOptions options = new JsonSerializerOptions
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    };

    public TagWebApiService(IHttpClientFactory factory, IConfiguration configuration)
    {
        this.httpClient = factory.CreateClient("ApiWithJwt");
        this.baseUrl = configuration["WebApiAddress"];
    }

    public async Task<Result> AddTag(long taskId, string tagName)
    {
        var route = "add-tag";
        var url = new Uri($"{this.baseUrl}{route}");
        var obj = new
        {
            taskId,
            tagName,
        };
        var result = await this.httpClient.PostAsJsonAsync(url, obj, this.options);

        if (result.StatusCode != System.Net.HttpStatusCode.OK)
        {
            var errorMessage = await result.Content.ReadAsStringAsync();
            return Result.Error("Could not add tag" + result.StatusCode + errorMessage);
        }

        return Result.Success();
    }

    public async Task<Result> DeleteTag(long taskId, long tagId)
    {
        var route = "delete-tag";
        var url = new Uri($"{this.baseUrl}{route}");
        var obj = new
        {
            taskId,
            tagId,
        };
        var result = await this.httpClient.PostAsJsonAsync(url, obj, this.options);

        if (result.StatusCode != System.Net.HttpStatusCode.OK)
        {
            var errorMessage = await result.Content.ReadAsStringAsync();
            return Result.Error("Could not delete tag" + result.StatusCode + errorMessage);
        }

        return Result.Success();
    }

    public async Task<ResultWithData<List<Tag?>?>> GetAllTags()
    {
        var route = "tags";
        var url = new Uri(this.baseUrl + route);
        var result = await this.httpClient.GetAsync(url);

        if (result.StatusCode != System.Net.HttpStatusCode.OK)
        {
            var errorMessage = await result.Content.ReadAsStringAsync();
            return ResultWithData<List<Tag?>?>.Error("boom!" + result.StatusCode + errorMessage);
        }

        var json = await result.Content.ReadAsStringAsync();

        var tags = JsonSerializer.Deserialize<List<TagWebApiModel?>?>(json, this.options);

        return ResultWithData<List<Tag?>?>.Success(
            [.. tags?.Select(t => t.ToDomain()) ?? Enumerable.Empty<Tag?>()],
            "successfully obtained");
    }

    public async Task<ResultWithData<List<TaskSummary?>?>> GetTasksByTag(long tagId)
    {
        var route = "tag";
        var url = new Uri(this.baseUrl + route + "?tagId=" + tagId);
        var result = await httpClient.GetAsync(url);

        if (!result.IsSuccessStatusCode)
        {
            return ResultWithData<List<TaskSummary?>?>.Error("something went wrong" + result.StatusCode);
        }

        var json = await result.Content.ReadAsStringAsync();

        var tasks = JsonSerializer.Deserialize<List<TaskSummaryWebApiModel?>?>(json, this.options);

        return ResultWithData<List<TaskSummary?>?>.Success(
            [.. tasks?.Select(t => t.ToDomain()) ?? Enumerable.Empty<TaskSummary?>()],
            "successfully obtained");
    }
}
