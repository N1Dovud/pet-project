using WebApp.Models.Enums;

namespace WebApp.Business.Helpers;

public class EditTaskStatus
{
    public int TaskId { get; set; }

    public ToDoListTaskStatus TaskStatus { get; set; }
}
