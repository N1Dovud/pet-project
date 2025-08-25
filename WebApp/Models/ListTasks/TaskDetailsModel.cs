using System.ComponentModel.DataAnnotations;
using WebApp.Models.Comments;
using WebApp.Models.Enums;
using WebApp.Models.Tags;

namespace WebApp.Models.ListTasks;

public class TaskDetailsModel
{
    public long Id { get; set; }

    [Required]
    public string? Title { get; set; }

    [Required]
    public string? Description { get; set; }

    public DateTime CreationDateTime { get; set; } = DateTime.Now;

    [Required]
    public DateTime DueDateTime { get; set; } = DateTime.Now;

    public ToDoListTaskStatus TaskStatus { get; set; } = ToDoListTaskStatus.NotStarted;

    public List<TagModel> Tags { get; set; } = [];

    public List<CommentModel> Comments { get; set; } = [];
}
