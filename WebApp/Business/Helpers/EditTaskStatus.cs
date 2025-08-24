using WebApp.Models.Enums;

namespace WebApp.Business.Helpers;

internal class EditTaskStatus
{
    public int TaskId { get; set; }

    public ToDoListTaskStatus TaskStatus { get; set; }
}
