using WebApp.Models.Enums;

namespace WebApp.Models.Helpers;

public class EditTaskStatusModel
{
    public int TaskId { get; set; }

    public ToDoListTaskStatus TaskStatus { get; set; }
}
