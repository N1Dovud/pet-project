using System.Collections.ObjectModel;
using WebApi.Models.Enums;
using WebApi.Models.Helpers;

namespace WebApi.Mappers;

internal static class HelpersMapper
{
    public static Collection<ToDoListTaskStatus> ToDomain(this StatusFilter status)
    {
        return status switch
        {
            StatusFilter.NotStarted => new Collection<ToDoListTaskStatus>([ToDoListTaskStatus.NotStarted]),
            StatusFilter.InProgress => new Collection<ToDoListTaskStatus>([ToDoListTaskStatus.InProgress]),
            StatusFilter.Active => new Collection<ToDoListTaskStatus>(
        [
            ToDoListTaskStatus.NotStarted,
            ToDoListTaskStatus.InProgress,
        ]),
            StatusFilter.Completed => new Collection<ToDoListTaskStatus>([ToDoListTaskStatus.Completed]),
            _ => throw new ArgumentOutOfRangeException(nameof(status), status, null)
        };
    }
}
