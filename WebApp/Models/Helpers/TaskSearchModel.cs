using WebApp.Models.ListTasks;

namespace WebApp.Models.Helpers;

public class TaskSearchModel
{
    public List<TaskSummaryModel?>? Tasks { get; set; }

    public string? ReturnUrl { get; set; }
}
