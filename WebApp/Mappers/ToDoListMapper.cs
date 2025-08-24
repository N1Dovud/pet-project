using WebApp.Business.ToDoLists;
using WebApp.Models.ToDoLists;

namespace WebApp.Mappers;

internal static class ToDoListMapper
{
    public static ToDoList? ToDomain(this ToDoListModel? list)
    {
        if (list == null)
        {
            return null;
        }

        return new ToDoList(list.Tasks.Select(t => t.ToDomain()))
        {
            Id = list.Id,
            Title = list.Title,
            Description = list.Description,
            OwnerId = list.OwnerId,
        };
    }

    public static ToDoList? ToDomain(this ToDoListWebApiModel list)
    {
        if (list == null)
        {
            return null;
        }

        return new ToDoList([])
        {
            Id = list.Id,
            Title = list.Title,
            Description = list.Description,
            OwnerId = list.OwnerId,
        };
    }

    public static ToDoListModel? ToDTO(this ToDoList list)
    {
        if (list == null)
        {
            return null;
        }

        return new ToDoListModel(list.Tasks.Select(t => t.ToModel()))
        {
            Id = list.Id,
            Title = list.Title,
            Description = list.Description,
            OwnerId = list.OwnerId,
        };
    }

    public static ToDoListWebApiModel? ToWebApiModel(this ToDoList? list)
    {
        if (list == null)
        {
            return null;
        }

        return new ToDoListWebApiModel
        {
            Id = list.Id,
            Title = list.Title,
            Description = list.Description,
            OwnerId = list.OwnerId,
        };
    }

    public static ToDoListModel? ToModel(this ToDoList? list)
    {
        if (list == null)
        {
            return null;
        }

        return new ToDoListModel(list.Tasks.Select(t => t.ToModel()))
        {
            Id = list.Id,
            Title = list.Title,
            Description = list.Description,
            OwnerId = list.OwnerId,
        };
    }
}
