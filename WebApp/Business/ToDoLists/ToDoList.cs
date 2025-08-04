using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WebApp.Services.Database.Entities;
using WebApp.Business.ListTasks;

namespace WebApp.Business.ToDoLists;

public class ToDoList
{
    public long? Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public List<TaskDetails> Tasks { get; set; } = [];

    public long OwnerId { get; set; }
}
