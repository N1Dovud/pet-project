using WebApp.Models.ListTasks;

namespace WebApp.Business.ListTasks;

public class ListTaskInfo
{
    public long ListId { get; set; }

    public string Title { get; set; } = string.Empty;

    public List<TaskSummary> Tasks { get; set; } = [];
}
