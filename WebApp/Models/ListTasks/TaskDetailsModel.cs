using WebApp.Models.Enums;
using WebApp.Business.Comments;
using WebApp.Models.Tags;
using WebApp.Models.Comments;
using System.ComponentModel.DataAnnotations;
using WebApp.Models.Enums;

namespace WebApp.Models.ListTasks;

public class TaskDetailsModel
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

    public List<TagModel> Tags { get; set; } = [];

    public List<CommentModel> Comments { get; set; } = [];
}
