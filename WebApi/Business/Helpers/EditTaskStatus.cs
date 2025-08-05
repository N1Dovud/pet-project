using WebApi.Models.Enums;

namespace WebApi.Business.Helpers;

public class EditTaskStatus
{
    public long TaskId { get; set; }

    public ToDoListTaskStatus TaskStatus { get; set; }
}
