using System.Collections.Generic;
using WebApp.Business;
using WebApp.Models;
using WebApp.Services.Database.Entities;

namespace WebApp.Mappers;

public static class ToDoListMapper
{
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

    public static ToDoListModel? ToDTO(this ToDoList list)
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

    public static ToDoList? ToDomain(this ToDoListModel? list)
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
            Tasks = list.Tasks,
            OwnerId = list.OwnerId,
        };
    }
}
