using WebApp.Business.Tags;
using WebApp.Models.Enums;

namespace WebApp.Business.ListTasks;

public class TaskSummary
{
    public TaskSummary(IEnumerable<Tag> tags)
    {
        this.Tags = tags.ToList().AsReadOnly();
    }

    public long Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public DateTime CreationDateTime { get; set; }

    public DateTime DueDateTime { get; set; }

    public ToDoListTaskStatus TaskStatus { get; set; }

    public IReadOnlyList<Tag> Tags { get; }
}
