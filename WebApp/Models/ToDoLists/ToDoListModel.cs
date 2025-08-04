using WebApp.Models.ListTasks;
using WebApp.Services.Database.Entities;

namespace WebApp.Models.ToDoLists;

public class ToDoListModel
{
    public long? Id { get; set; }

    public required string Title { get; set; }

    public required string Description { get; set; }

    public List<TaskDetailsModel> Tasks { get; set; } = [];

    public long OwnerId { get; set; }
}
