using WebApp.Models.Comments;
using WebApp.Models.ListTasks;
using WebApp.Models.Tags;

namespace WebApp.Models.Helpers;

internal class TaskSearchModel
{
    public TaskSearchModel(IEnumerable<TaskSummaryModel?> tasks)
    {
        this.Tasks = tasks.ToList().AsReadOnly();
    }

    public IReadOnlyList<TaskSummaryModel?>? Tasks { get; } =[];

    public string? ReturnUrl { get; set; }
}
