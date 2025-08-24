using System.Runtime.CompilerServices;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Helpers;

internal static class Authenticate
{
    public static long? GetUserId(this ControllerBase controller)
    {
        if (controller == null)
        {
            return null;
        }

        var idString = controller.User.FindFirst("userId")?.Value;

        Console.WriteLine($"UserId: {idString}");
        if (long.TryParse(idString, out var userId))
        {
            return userId;
        }
        else
        {
            return null;
        }
    }
}
