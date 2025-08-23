using System.Text.Json;
using System.Threading.Tasks;
using WebApp.Common;

namespace WebApp.Services.CommentService;

public class CommentWebApiService : ICommentWebApiService
{
    private readonly HttpClient httpClient;
    private readonly string baseUrl;
    private readonly JsonSerializerOptions options = new JsonSerializerOptions
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    };

    public CommentWebApiService(IHttpClientFactory factory, IConfiguration configuration)
    {
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

        if (result.StatusCode != System.Net.HttpStatusCode.OK)
        {
            var errorMessage = await result.Content.ReadAsStringAsync();
            return Result.Error("Could not add comment" + result.StatusCode + errorMessage);
        }

        return Result.Success();
    }

    public async Task<Result> DeleteComment(long commentId)
    {
        var route = "delete-comment";
        var url = new Uri($"{this.baseUrl}{route}");
        var obj = new
        {
            commentId,
        };
        var result = await this.httpClient.PostAsJsonAsync(url, obj, this.options);

        if (result.StatusCode != System.Net.HttpStatusCode.OK)
        {
            var errorMessage = await result.Content.ReadAsStringAsync();
            return Result.Error("Could not delete comment" + result.StatusCode + errorMessage);
        }

        return Result.Success();
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
        var result = await this.httpClient.PostAsJsonAsync(url, obj, this.options);

        if (result.StatusCode != System.Net.HttpStatusCode.OK)
        {
            var errorMessage = await result.Content.ReadAsStringAsync();
            return Result.Error("Could not edit comment" + result.StatusCode + errorMessage);
        }

        return Result.Success();
    }
}
