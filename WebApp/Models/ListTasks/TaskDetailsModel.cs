using System.ComponentModel.DataAnnotations;
using WebApp.Models.Comments;
using WebApp.Models.Enums;
using WebApp.Models.Tags;

namespace WebApp.Models.ListTasks;

internal class TaskDetailsModel
{
    public TaskDetailsModel(IEnumerable<TagModel> tags, IEnumerable<CommentModel> comments)
    {
        this.Tags = tags.ToList().AsReadOnly();
        this.Comments = comments.ToList().AsReadOnly();
    }

    public long Id { get; set; }

    [Required]
    public string Title { get; set; } = default!;

    [Required]
    public string Description { get; set; } = default!;

    public DateTime CreationDateTime { get; set; } = DateTime.Now;

    [Required]
    public DateTime DueDateTime { get; set; }

    public ToDoListTaskStatus TaskStatus { get; set; } = ToDoListTaskStatus.NotStarted;

    public IReadOnlyList<TagModel> Tags { get; } =[];

    public IReadOnlyList<CommentModel> Comments { get; } =[];
}
