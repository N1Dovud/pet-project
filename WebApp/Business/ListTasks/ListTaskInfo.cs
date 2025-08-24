using WebApp.Models.ListTasks;

namespace WebApp.Business.ListTasks;

internal class ListTaskInfo
{
    public ListTaskInfo(IEnumerable<TaskSummary> summaries)
    {
        this.Tasks = summaries.ToList().AsReadOnly();
    }

    public long ListId { get; set; }

    public string Title { get; set; } = string.Empty;

    public IReadOnlyList<TaskSummary> Tasks { get; } =[];
}
