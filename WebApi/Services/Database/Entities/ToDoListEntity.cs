using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Services.Database.Entities;

[Table("todo_list")]
public class ToDoListEntity
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Required]
    [MaxLength(100)]
    [Column("title")]
    public string Title { get; set; } = string.Empty;

    [Required]
    [Column("description")]
    public string Description { get; set; } = string.Empty;

    public List<ToDoListTaskEntity> Tasks { get; set; } = [];

    [Required]
    [Column("owner_id")]
    public long OwnerId { get; set; }
}
