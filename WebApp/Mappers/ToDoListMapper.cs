using WebApp.Business.ToDoLists;
using WebApp.Models.ToDoLists;

namespace WebApp.Mappers;

internal static class ToDoListMapper
{
    public static ToDoList ToDomain(this ToDoListModel list)
    {
        ArgumentNullException.ThrowIfNull(list);

        return new ToDoList
        {
            Id = list.Id,
            Title = list.Title,
            Description = list.Description,
            OwnerId = list.OwnerId,
            Tasks = [.. list.Tasks.Select(t => t.ToDomain())],
        };
    }

    public static ToDoList? ToDomain(this ToDoListWebApiModel list)
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
            OwnerId = list.OwnerId,
        };
    }

    public static ToDoListModel ToDTO(this ToDoList list)
    {
        ArgumentNullException.ThrowIfNull(list);

        return new ToDoListModel
        {
            Id = list.Id,
            Title = list.Title,
            Description = list.Description,
            OwnerId = list.OwnerId,
            Tasks = [.. list.Tasks.Select(t => t.ToModel())],
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

        return new ToDoListModel
        {
            Id = list.Id,
            Title = list.Title,
            Description = list.Description,
            OwnerId = list.OwnerId,
            Tasks = [.. list.Tasks.Select(t => t.ToModel())],
        };
    }
}
