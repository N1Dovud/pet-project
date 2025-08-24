namespace WebApi.Models.ToDoLists;

internal class ToDoListModel
{
    public long? Id { get; set; }

    required public string Title { get; set; }

    required public string Description { get; set; }

    public long OwnerId { get; set; }
}
