using Microsoft.AspNetCore.Mvc;
using WebApp.Services.DatabaseService;

namespace WebApp.Helpers;

public static class HttpResponseHelper
{
    public static IActionResult ToHttpResponse(this ControllerBase controller, Result result)
    {
        return result.Status switch
        {
            ResultStatus.Success => result.Message is null
                ? controller.Ok()
                : controller.Ok(result.Message),
            ResultStatus.NotFound => result.Message is null
                ? controller.NotFound()
                : controller.NotFound(new { error = result.Message }),

            ResultStatus.Forbidden => result.Message is null
                ? controller.Forbid()
                : controller.StatusCode(403, new { error = result.Message }),
            ResultStatus.Error => result.Message is null
                ? controller.BadRequest()
                : controller.BadRequest(new { error = result.Message }),

            _ => controller.StatusCode(500, new { error = result.Message ?? "Unexpected error" })
        };
    }

    public static IActionResult StatusWithOptionalMessage(this ControllerBase controller, int statusCode, string? message)
    {
        return string.IsNullOrWhiteSpace(message)
            ? controller.StatusCode(statusCode)
            : controller.StatusCode(statusCode, message);
    }
}
