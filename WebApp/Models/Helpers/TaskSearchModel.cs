using WebApp.Models.ListTasks;

namespace WebApp.Models.Helpers;

internal class TaskSearchModel
{
    public TaskSearchModel(IEnumerable<TaskSummaryModel?> tasks)
    {
        this.Tasks = tasks.ToList().AsReadOnly();
    }

    public IReadOnlyList<TaskSummaryModel?>? Tasks { get; }

    public string? ReturnUrl { get; set; }
}
