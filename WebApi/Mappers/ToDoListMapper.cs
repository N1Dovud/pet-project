using WebApi.Business;
using WebApi.Services.Database.Entities;

namespace WebApi.Mappers;

public static class ToDoListMapper
{
    public static ToDoList ToDomain(this ToDoListEntity entity)
    {
        if (entity == null)
        {
            return null!;
        }

        return new ToDoList
        {
            Id = entity.Id,
            Title = entity.Title,
            Description = entity.Description,
            Tasks = entity.Tasks,
            Permissions = entity.Permissions,
        };
    }
}
