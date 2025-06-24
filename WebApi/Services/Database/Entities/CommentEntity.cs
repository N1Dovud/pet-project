using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Services.Database.Entities;

[Table("comment")]
public class CommentEntity
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Required]
    [Column("text_note")]
    public string Note { get; set; } = string.Empty;

    [Required]
    [ForeignKey("ToDoListTaskEntity")]
    [Column("task_id")]
    public long TaskId { get; set; }

    public ToDoListTaskEntity? Task { get; set; }

    [Required]
    [Column("user_id")]
    public long UserId { get; set; }

    [Required]
    [Column("creation_date_time")]
    public DateTime CreationDateTime { get; set; } = DateTime.Now;

    [Required]
    [Column("last_edit_date_time")]
    public DateTime LastEditDateTime { get; set; } = DateTime.Now;
}
