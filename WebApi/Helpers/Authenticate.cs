using System.Runtime.CompilerServices;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Helpers;

public static class Authenticate
{
    public static long? GetUserId(this ControllerBase controller)
    {
        if (controller == null)
        {
            return null;
        }

        var idString = controller.User.FindFirst("userId")?.Value;
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
