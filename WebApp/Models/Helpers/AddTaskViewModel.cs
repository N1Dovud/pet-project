using WebApp.Models.ListTasks;

namespace WebApp.Models.Helpers;

internal class AddTaskViewModel
{
    public long ListId { get; set; }

    public TaskDetailsModel? TaskDetails { get; set; }
}
