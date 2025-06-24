using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApi.Models.Enums;

namespace WebApi.Models.Entities;

[Table("todo_list_permission")]
public class ToDoListPermissionEntity
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Required]
    [Column("todo_list_id")]
    [ForeignKey("ToDoListEntity")]
    public long ToDoListId { get; set; }

    public ToDoListEntity? ToDoList { get; set; }

    [Required]
    [Column("user_id")]
    public long UserId { get; set; }

    [Required]
    [Column("access_level")]
    public ToDoListAccessLevel AccessLevel { get; set; }
}
