namespace WebApi.Business.ListTasks;

public class ListTaskInfo
{
    public ListTaskInfo(IEnumerable<TaskSummary>? summaries)
    {
        this.Tasks = (summaries ?? Enumerable.Empty<TaskSummary>()).ToList().AsReadOnly();
    }

    public long ListId { get; set; }

    public string Title { get; set; } = string.Empty;

    public IReadOnlyList<TaskSummary> Tasks { get; }
}
