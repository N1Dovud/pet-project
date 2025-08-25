using WebApi.Business.Comments;
using WebApi.Business.Tags;
using WebApi.Models.Enums;

namespace WebApi.Business.ListTasks;

public class TaskDetails
{
    public long Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public DateTime CreationDateTime { get; set; }

    public DateTime DueDateTime { get; set; }

    public ToDoListTaskStatus TaskStatus { get; set; }

    public List<Tag> Tags { get; set; } = [];

    public List<Comment> Comments { get; set; } = [];
}
