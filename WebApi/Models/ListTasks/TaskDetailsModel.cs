using WebApi.Models.Enums;
using WebApi.Business.Comments;
using WebApi.Models.Tags;
using WebApi.Models.Comments;

namespace WebApi.Models.ListTasks;

public class TaskDetailsModel
{
    public long Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public DateTime CreationDateTime { get; set; }

    public DateTime DueDateTime { get; set; }

    public ToDoListTaskStatus TaskStatus { get; set; }

    public List<TagModel> Tags { get; set; } = [];

    public List<CommentModel> Comments { get; set; } = [];
}
