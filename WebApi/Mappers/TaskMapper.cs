using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Business.Comments;
using WebApi.Business.Helpers;
using WebApi.Business.ListTasks;
using WebApi.Business.Tags;
using WebApi.Business.ToDoLists;
using WebApi.Models.Comments;
using WebApi.Models.Helpers;
using WebApi.Models.ListTasks;
using WebApi.Models.Tags;
using WebApi.Models.ToDoLists;
using WebApi.Services.Database.Entities;

namespace WebApi.Mappers;

internal static class TaskMapper
{
    public static ListTaskInfo? ToListTask(this ToDoListEntity list)
    {
        if (list == null)
        {
            return null;
        }

        return new ListTaskInfo(list.Tasks?.ConvertAll(task => task.ToTaskSummary()) ?? [])
        {
            ListId = list.Id,
            Title = list.Title,
        };
    }

    public static TaskSummary ToTaskSummary(this ToDoListTaskEntity task)
    {
        ArgumentNullException.ThrowIfNull(task);
        return new TaskSummary(task.Tags.ConvertAll(t => t.ToModel()))
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            CreationDateTime = task.CreationDateTime,
            DueDateTime = task.DueDateTime,
            TaskStatus = task.TaskStatus,
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

    public static TaskDetails ToTaskDetails(this ToDoListTaskEntity task)
    {
        ArgumentNullException.ThrowIfNull(task);
        return new TaskDetails(
            [.. task.Tags.Select(t => t.ToDomain())],
            [.. task.Comments.Select(c => c.ToDomain())])
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            CreationDateTime = task.CreationDateTime,
            DueDateTime = task.DueDateTime,
            TaskStatus = task.TaskStatus,
        };
    }

    public static TaskSummary? ToDomain(this ToDoListTaskEntity task)
    {
        ArgumentNullException.ThrowIfNull(task);

        return new TaskSummary([.. task.Tags.Select(t => t.ToDomain())])
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            CreationDateTime = task.CreationDateTime,
            DueDateTime = task.DueDateTime,
            TaskStatus = task.TaskStatus,
        };
    }

    public static Tag ToDomain(this TagEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        return new Tag
        {
            Id = entity.Id,
            Name = entity.Name,
        };
    }

    public static Comment ToDomain(this CommentEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        return new Comment
        {
            Id = entity.Id,
            CreationDateTime = entity.CreationDateTime,
            LastEditDateTime = entity.LastEditDateTime,
            Note = entity.Note,
        };
    }

    public static TaskDetails ToDomain(this TaskDetailsModel model)
    {
        ArgumentNullException.ThrowIfNull(model);
        return new TaskDetails([],[])
        {
            Id = model.Id,
            Title = model.Title,
            Description = model.Description,
            CreationDateTime = model.CreationDateTime,
            DueDateTime = model.DueDateTime,
            TaskStatus = model.TaskStatus,
        };
    }

    public static EditTaskStatus? ToDomain(this EditTaskStatusModel model)
    {
        if (model == null)
        {
            return null;
        }

        return new EditTaskStatus
        {
            TaskId = model.TaskId,
            TaskStatus = model.TaskStatus,
        };
    }

    public static CommentModel ToModel(this Comment entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        return new CommentModel
        {
            Id = entity.Id,
            CreationDateTime = entity.CreationDateTime,
            LastEditDateTime = entity.LastEditDateTime,
            Note = entity.Note,
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
        return new ListTaskInfoModel(list.Tasks.Select(task => task.ToModel()))
        {
            ListId = list.ListId,
            Title = list.Title,
        };
    }

    public static TaskSummaryModel ToModel(this TaskSummary? taskSummary)
    {
        ArgumentNullException.ThrowIfNull(taskSummary);
        return new TaskSummaryModel(taskSummary.Tags.Select(tag => tag.ToModel()))
        {
            Id = taskSummary.Id,
            Title = taskSummary.Title,
            Description = taskSummary.Description,
            CreationDateTime = taskSummary.CreationDateTime,
            DueDateTime = taskSummary.DueDateTime,
            TaskStatus = taskSummary.TaskStatus,
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

    public static TaskDetailsModel ToModel(this TaskDetails task)
    {
        ArgumentNullException.ThrowIfNull(task);
        return new TaskDetailsModel(
            task.Tags.Select(t => t.ToModel()),
            task.Comments.Select(c => c.ToModel()))
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
