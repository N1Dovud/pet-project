using WebApi.Models.Enums;

namespace WebApi.Models.Helpers;

public class EditTaskStatusModel
{
    public long TaskId { get; set; }

    public ToDoListTaskStatus TaskStatus { get; set; }
}
