using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Services.Database.Entities;

[Table("tag")]
[Index(nameof(Name), IsUnique = true)]
public class TagEntity
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Required]
    [Column("name")]
    public string Name { get; set; } = string.Empty;

    public List<ToDoListTaskEntity> Tasks { get; set; } = [];
}
