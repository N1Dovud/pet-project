using WebApp.Models.ListTasks;

namespace WebApp.Models.Helpers;

public class TaskViewModel
{
    public string? ReturnUrl { get; set; }

    public TaskDetailsModel? TaskDetails { get; set; }

    public bool IsOwner { get; set; }
}
