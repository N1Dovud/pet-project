using WebApp.Models.ListTasks;

namespace WebApp.Models.ToDoLists;

public class ToDoListModel
{
    public long? Id { get; set; }

    required public string Title { get; set; }

    required public string Description { get; set; }

    public List<TaskDetailsModel> Tasks { get; set; } = [];

    public long OwnerId { get; set; }
}
