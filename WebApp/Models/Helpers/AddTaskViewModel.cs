using WebApp.Models.ListTasks;

namespace WebApp.Models.Helpers;

public class AddTaskViewModel
{
    public long ListId { get; set; }

    public TaskDetailsModel? TaskDetails { get; set; }
}
