using System.ComponentModel.DataAnnotations;
using WebApi.Models.Comments;
using WebApi.Models.Enums;
using WebApi.Models.Tags;

namespace WebApi.Models.ListTasks;

public class TaskDetailsModel
{
    public TaskDetailsModel(IEnumerable<TagModel>? tags, IEnumerable<CommentModel>? comments)
    {
        this.Tags = (tags ?? Enumerable.Empty<TagModel>()).ToList().AsReadOnly();
        this.Comments = (comments ?? Enumerable.Empty<CommentModel>()).ToList().AsReadOnly();
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

    public IReadOnlyList<TagModel> Tags { get; }

    public IReadOnlyList<CommentModel> Comments { get; }
}
