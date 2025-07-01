using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WebApi.Services.Database.Entities;

namespace WebApi.Models.ToDoLists;

public class ToDoListModel
{
    public long? Id { get; set; }

    public required string Title { get; set; }

    public required string Description { get; set; }

    public long OwnerId { get; set; }
}
