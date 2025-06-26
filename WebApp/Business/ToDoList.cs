using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WebApp.Services.Database.Entities;

namespace WebApp.Business;

public class ToDoList
{
    public long? Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public List<ToDoListTaskEntity> Tasks { get; set; } = [];

    public long OwnerId { get; set; }
}
