namespace WebApi.Models.ToDoLists;

public class ToDoListModel
{
    public long? Id { get; set; }

    required public string Title { get; set; }

    required public string Description { get; set; }

    public long OwnerId { get; set; }
}
