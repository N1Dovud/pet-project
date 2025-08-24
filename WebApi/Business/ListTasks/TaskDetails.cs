using System.Collections.ObjectModel;
using WebApi.Business.Comments;
using WebApi.Business.Tags;
using WebApi.Models.Enums;

namespace WebApi.Business.ListTasks;

internal class TaskDetails
{
    public TaskDetails(IEnumerable<Tag> tags, IEnumerable<Comment> comments)
    {
        this.Tags = tags.ToList().AsReadOnly();
        this.Comments = comments.ToList().AsReadOnly();
    }

    public long Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public DateTime CreationDateTime { get; set; }

    public DateTime DueDateTime { get; set; }

    public ToDoListTaskStatus TaskStatus { get; set; }

    public IReadOnlyList<Tag> Tags { get; } =[];

    public IReadOnlyList<Comment> Comments { get; } =[];
}
