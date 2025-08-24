using WebApp.Models.Enums;

namespace WebApp.Models.Helpers;

internal class EditTaskStatusModel
{
    public int TaskId { get; set; }

    public ToDoListTaskStatus TaskStatus { get; set; }
}
