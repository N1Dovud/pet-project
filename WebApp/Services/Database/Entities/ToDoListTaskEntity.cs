using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApp.Models.Enums;

namespace WebApp.Services.Database.Entities;

[Table("todo_task")]
public class ToDoListTaskEntity
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Required]
    [Column("title")]
    public string Title { get; set; } = string.Empty;

    [Required]
    [Column("description")]
    public string Description { get; set; } = string.Empty;

    [Required]
    [Column("creation_date_time")]
    public DateTime CreationDateTime { get; set; }

    [Required]
    [Column("due_date_time")]
    public DateTime DueDateTime { get; set; }

    [Required]
    [Column("task_status")]
    public ToDoListTaskStatus TaskStatus { get; set; }

    [Required]
    [Column("assignee")]
    public long Assignee { get; set; }

    public List<TagEntity> Tags { get; set; } = [];

    [Required]
    [ForeignKey("ToDoListEntity")]
    [Column("todo_list_id")]

    public long ToDoListId { get; set; }

    [Required]
    public ToDoListEntity? ToDoList { get; set; }

    public List<CommentEntity> Comments { get; set; } = [];
}
