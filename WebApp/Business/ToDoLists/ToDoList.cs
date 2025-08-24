using WebApp.Business.ListTasks;

namespace WebApp.Business.ToDoLists;

public class ToDoList
{
    public ToDoList(IEnumerable<TaskDetails> tasks)
    {
        this.Tasks = tasks.ToList().AsReadOnly();
    }

    public long? Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public IReadOnlyList<TaskDetails> Tasks { get; }

    public long OwnerId { get; set; }
}
