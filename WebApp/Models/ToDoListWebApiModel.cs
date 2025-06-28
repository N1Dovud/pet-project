using WebApp.Services.Database.Entities;

namespace WebApp.Models;

public class ToDoListWebApiModel
{
    public long? Id { get; set; }

    public required string Title { get; set; }

    public required string Description { get; set; }

    public long OwnerId { get; set; }
}
