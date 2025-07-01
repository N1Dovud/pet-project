using System.Collections.Generic;
using WebApi.Business.ToDoLists;
using WebApi.Models.ToDoLists;
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
            OwnerId = entity.OwnerId,
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
            OwnerId = list.OwnerId,
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
            OwnerId = list.OwnerId,
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
            Title = list.Title,
            Description = list.Description,
            OwnerId = list.OwnerId,
        };
    }
}
