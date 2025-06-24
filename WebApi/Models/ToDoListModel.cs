using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WebApi.Services.Database.Entities;

namespace WebApi.Models;

public class ToDoListModel
{
    public long Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public List<ToDoListTaskEntity> Tasks { get; set; } = [];

    public List<ToDoListPermissionEntity> Permissions { get; set; } = [];
}
