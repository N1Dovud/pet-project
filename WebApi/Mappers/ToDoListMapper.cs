using WebApi.Business;
using WebApi.Models;
using WebApi.Services.Database.Entities;

namespace WebApi.Mappers;

public static class ToDoListMapper
{
    public static ToDoList? ToDomain(this ToDoListEntity entity)
    {
        if (entity == null)
        {
            return null;
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

    public static ToDoList? ToDomain(this ToDoListModel list)
    {
        if (list == null)
        {
            return null;
        }

        return new ToDoList
        {
            Id = list.Id,
            Title = list.Title,
            Description = list.Description,
            Tasks = list.Tasks,
            Permissions = list.Permissions,
        };
    }

    public static ToDoListModel? ToModel(this ToDoList list)
    {
        if (list == null)
        {
            return null;
        }

        return new ToDoListModel
        {
            Id = list.Id,
            Title = list.Title,
            Description = list.Description,
            Tasks = list.Tasks,
            Permissions = list.Permissions,
        };
    }

    public static ToDoListEntity? ToEntity(this ToDoList list)
    {
        if (list == null)
        {
            return null;
        }

        return new ToDoListEntity
        {
            Id = list.Id,
            Title = list.Title,
            Description = list.Description,
            Tasks = list.Tasks,
            Permissions = list.Permissions,
        };
    }
}
