using WebApp.Models.Comments;
using WebApp.Models.Enums;
using WebApp.Models.Tags;

namespace WebApp.Models.ListTasks;

internal class TaskSummaryModel
{
    public TaskSummaryModel(IEnumerable<TagModel> tags)
    {
        this.Tags = tags.ToList().AsReadOnly();
    }

    public long Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public DateTime CreationDateTime { get; set; }

    public DateTime DueDateTime { get; set; }

    public ToDoListTaskStatus TaskStatus { get; set; }

    public IReadOnlyList<TagModel> Tags { get; } =[];
}
