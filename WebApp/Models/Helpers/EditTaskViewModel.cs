using WebApp.Models.ListTasks;

namespace WebApp.Models.Helpers;

public class EditTaskViewModel
{
    public long listId { get; set; }

    public TaskDetailsModel? taskDetails { get; set; }
}
