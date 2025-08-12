using System.Text.Json;
using WebApp.Business.Tags;
using WebApp.Common;
using WebApp.Mappers;
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

        var tags = JsonSerializer.Deserialize<List<TagWebApiModel>>(json, this.options);

        return ResultWithData<List<Tag?>?>.Success([.. tags?.Select(t => t.ToDomain()) ?? []], "successfully obtained");
    }
}
