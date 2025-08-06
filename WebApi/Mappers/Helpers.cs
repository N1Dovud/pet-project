using WebApi.Models.Enums;
using WebApi.Models.Helpers;

namespace WebApi.Mappers;

public static class Helpers
{
    public static List<ToDoListTaskStatus> ToDomain(this StatusFilter status)
    {
        return status switch
        {
            StatusFilter.NotStarted => new() { ToDoListTaskStatus.NotStarted },
            StatusFilter.InProgress => new() { ToDoListTaskStatus.InProgress },
            StatusFilter.Active => new() { ToDoListTaskStatus.NotStarted, ToDoListTaskStatus.InProgress },
            StatusFilter.Completed => new() { ToDoListTaskStatus.Completed },
            _ => throw new ArgumentOutOfRangeException(nameof(status), status, null)
        };
    }
}
