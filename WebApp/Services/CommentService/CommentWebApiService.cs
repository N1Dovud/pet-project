using System.Text.Json;
using WebApp.Common;
using WebApp.Helpers;

namespace WebApp.Services.CommentService;

internal class CommentWebApiService : ICommentWebApiService
{
    private readonly HttpClient httpClient;
    private readonly string? baseUrl;
    private readonly JsonSerializerOptions options = new JsonSerializerOptions
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    };

    public CommentWebApiService(IHttpClientFactory factory, IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(factory);
        this.httpClient = factory.CreateClient("ApiWithJwt");
        this.baseUrl = configuration["WebApiAddress"];
    }

    public async Task<Result> AddComment(long taskId, string note)
    {
        var route = "add-comment";
        var url = new Uri($"{this.baseUrl}{route}");
        var obj = new
        {
            taskId,
            note,
        };
        var result = await this.httpClient.PostAsJsonAsync(url, obj, this.options);

        return await HttpResponseMapper.MapHttpResponseToResult(result);
    }

    public async Task<Result> DeleteComment(long commentId)
    {
        var route = "delete-comment";
        var url = new Uri($"{this.baseUrl}{route}?commendId={commentId}");
        var result = await this.httpClient.DeleteAsync(url);

        return await HttpResponseMapper.MapHttpResponseToResult(result);
    }

    public async Task<Result> EditComment(long commentId, string note)
    {
        var route = "edit-comment";
        var url = new Uri($"{this.baseUrl}{route}");
        var obj = new
        {
            commentId,
            note,
        };
        var result = await this.httpClient.PutAsJsonAsync(url, obj, this.options);

        return await HttpResponseMapper.MapHttpResponseToResult(result);
    }
}
