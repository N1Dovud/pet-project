using System.Net;
using System.Text.Json;
using WebApp.Common;

namespace WebApp.Helpers;

internal static class HttpResponseMapper
{
    public static async Task<Result> MapHttpResponseToResult(HttpResponseMessage response)
    {
        if (response.IsSuccessStatusCode)
        {
            return Result.Success();
        }

        var errorMessage = await ExtractErrorMessage(response);

        return response.StatusCode switch
        {
            HttpStatusCode.NotFound => Result.NotFound(errorMessage),
            HttpStatusCode.Forbidden => Result.Forbidden(errorMessage),
            HttpStatusCode.Unauthorized => Result.Unauthorized(errorMessage),
            HttpStatusCode.BadRequest => Result.Error(errorMessage),
            _ => Result.Error(errorMessage ?? $"HTTP {(int)response.StatusCode}: {response.ReasonPhrase}")
        };
    }

    public static async Task<ResultWithData<T>> MapHttpResponseToResult<T>(HttpResponseMessage response)
    {
        if (response.IsSuccessStatusCode)
        {
            throw new InvalidOperationException("This method should only be called for error responses");
        }

        var errorMessage = await ExtractErrorMessage(response);

        return response.StatusCode switch
        {
            HttpStatusCode.NotFound => ResultWithData<T>.NotFound(errorMessage),
            HttpStatusCode.Forbidden => ResultWithData<T>.Forbidden(errorMessage),
            HttpStatusCode.Unauthorized => ResultWithData<T>.Unauthorized(errorMessage),
            HttpStatusCode.BadRequest => ResultWithData<T>.Error(errorMessage),
            _ => ResultWithData<T>.Error(errorMessage ?? $"HTTP {(int)response.StatusCode}: {response.ReasonPhrase}")
        };
    }

    private static async Task<string?> ExtractErrorMessage(HttpResponseMessage response)
    {
        try
        {
            var content = await response.Content.ReadAsStringAsync();

            // Try to parse as JSON to extract error message
            if (content.StartsWith('{'))
            {
                var errorObj = JsonSerializer.Deserialize<JsonElement>(content);
                if (errorObj.TryGetProperty("error", out var errorProp))
                {
                    return errorProp.GetString();
                }
            }

            return content;
        }
        catch (Exception ex) when (ex is JsonException or InvalidOperationException)
        {
            return response.ReasonPhrase;
        }
    }
}
