using System.Collections.Generic;
using WebApi.Business.ListTasks;
using WebApi.Business.Tags;
using WebApi.Business.ToDoLists;
using WebApi.Models.ListTasks;
using WebApi.Models.Tags;
using WebApi.Models.ToDoLists;
using WebApi.Services.Database.Entities;

namespace WebApi.Mappers;

public static class TaskMapper
{
    public static ListTaskInfo? ToListTask(this ToDoListEntity list)
    {
        if (list == null)
        {
            return null;
        }

        return new ListTaskInfo
        {
            ListId = list.Id,
            Title = list.Title,
            Tasks = list.Tasks?.ConvertAll(task => task.ToTaskSummary()) ?? [],
        };
    }

    public static TaskSummary ToTaskSummary(this ToDoListTaskEntity task)
    {
        ArgumentNullException.ThrowIfNull(task);
        return new TaskSummary
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            CreationDateTime = task.CreationDateTime,
            DueDateTime = task.DueDateTime,
            TaskStatus = task.TaskStatus,
            Tags = task.Tags.ConvertAll(t => t.ToModel()) ?? [],
        };
    }

    public static Tag ToModel(this TagEntity tag)
    {
        ArgumentNullException.ThrowIfNull(tag);
        return new Tag
        {
            Id = tag.Id,
            Name = tag.Name,
        };
    }

    public static ListTaskInfoModel ToModel(this ListTaskInfo list)
    {
        ArgumentNullException.ThrowIfNull(list);
        return new ListTaskInfoModel
        {
            ListId = list.ListId,
            Title = list.Title,
            Tasks = list.Tasks.ConvertAll(task => task.ToModel()),
        };
    }

    public static TaskSummaryModel ToModel(this TaskSummary taskSummary)
    {
        ArgumentNullException.ThrowIfNull(taskSummary);
        return new TaskSummaryModel
        {
            Id = taskSummary.Id,
            Title = taskSummary.Title,
            Description = taskSummary.Description,
            CreationDateTime = taskSummary.CreationDateTime,
            DueDateTime = taskSummary.DueDateTime,
            TaskStatus = taskSummary.TaskStatus,
            Tags = taskSummary.Tags.ConvertAll(tag => tag.ToModel()),
        };
    }

    public static TagModel ToModel(this Tag tag)
    {
        ArgumentNullException.ThrowIfNull(tag);
        return new TagModel
        {
            Id = tag.Id,
            Name = tag.Name,
        };
    }

    public static ToDoListTaskEntity ToEntity(this TaskDetails task, long userId)
    {
        ArgumentNullException.ThrowIfNull(task);
        return new ToDoListTaskEntity
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            CreationDateTime = task.CreationDateTime,
            DueDateTime = task.DueDateTime,
            TaskStatus = task.TaskStatus,
            Assignee = userId,
        };
    }

    public static TaskDetails ToDomain(this TaskDetailsModel model)
    {
        ArgumentNullException.ThrowIfNull(model);
        return new TaskDetails
        {
            Id = model.Id,
            Title = model.Title,
            Description = model.Description,
            CreationDateTime = model.CreationDateTime,
            DueDateTime = model.DueDateTime,
            TaskStatus = model.TaskStatus,
        };
    }

    public static TaskDetails ToTaskDetails(this ToDoListTaskEntity task)
    {
        ArgumentNullException.ThrowIfNull(task);
        return new TaskDetails
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            CreationDateTime = task.CreationDateTime,
            DueDateTime = task.DueDateTime,
            TaskStatus = task.TaskStatus,
        };
    }

    public static TaskDetailsModel ToModel(this TaskDetails task)
    {
        ArgumentNullException.ThrowIfNull(task);
        return new TaskDetailsModel
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            CreationDateTime = task.CreationDateTime,
            DueDateTime = task.DueDateTime,
            TaskStatus = task.TaskStatus,
        };
    }
}
