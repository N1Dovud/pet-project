using WebApp.Models.ListTasks;

namespace WebApp.Models.ToDoLists;

internal class ToDoListModel
{
    public ToDoListModel(IEnumerable<TaskDetailsModel> tasks)
    {
        this.Tasks = tasks.ToList().AsReadOnly();
    }

    public long? Id { get; set; }

    required public string Title { get; set; }

    required public string Description { get; set; }

    public IReadOnlyList<TaskDetailsModel> Tasks { get; } =[];

    public long OwnerId { get; set; }
}
