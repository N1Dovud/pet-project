using System.ComponentModel.DataAnnotations;
using WebApp.Models.Comments;
using WebApp.Models.Enums;
using WebApp.Models.Tags;

namespace WebApp.Models.ListTasks;

public class TaskDetailsWebApiModel
{
    public long Id { get; set; }

    [Required]
    public string Title { get; set; } = default!;

    [Required]
    public string Description { get; set; } = default!;

    public DateTime CreationDateTime { get; set; } = DateTime.Now;

    [Required]
    public DateTime DueDateTime { get; set; }

    public ToDoListTaskStatus TaskStatus { get; set; } = ToDoListTaskStatus.NotStarted;

    public List<TagWebApiModel> Tags { get; set; } = [];

    public List<CommentWebApiModel> Comments { get; set; } = [];
}
