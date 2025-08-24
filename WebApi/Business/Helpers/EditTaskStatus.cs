using WebApi.Models.Enums;

namespace WebApi.Business.Helpers;

internal class EditTaskStatus
{
    public long TaskId { get; set; }

    public ToDoListTaskStatus TaskStatus { get; set; }
}
