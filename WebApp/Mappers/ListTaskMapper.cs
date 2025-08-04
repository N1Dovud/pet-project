using System.ComponentModel.DataAnnotations;
using WebApp.Business.Comments;
using WebApp.Business.ListTasks;
using WebApp.Business.Tags;
using WebApp.Models.Comments;
using WebApp.Models.ListTasks;
using WebApp.Models.Tags;
using WebApp.Services.Database.Entities;

namespace WebApp.Mappers;

public static class ListTaskMapper
{
    public static ListTaskInfo? ToDomain(this ListTaskInfoWebApiModel? taskInfo)
    {
        if (taskInfo == null)
        {
            return null;
        }

        return new ListTaskInfo
        {
            ListId = taskInfo.ListId,
            Title = taskInfo.Title,
            Tasks = taskInfo.Tasks.Select(t => t.ToDomain()).ToList(),
        };
    }

    public static TaskSummary ToDomain(this TaskSummaryWebApiModel taskSummary)
    {
        return new TaskSummary
        {
            Id = taskSummary.Id,
            Title = taskSummary.Title,
            Description = taskSummary.Description,
            CreationDateTime = taskSummary.CreationDateTime,
            DueDateTime = taskSummary.DueDateTime,
            TaskStatus = taskSummary.TaskStatus,
            Tags = taskSummary.Tags.Select(t => t.ToDomain()).ToList(),
        };
    }

    public static Tag ToDomain(this TagWebApiModel tag)
    {
        return new Tag
        {
            Id = tag.Id,
            Name = tag.Name,
        };
    }

    public static ListTaskInfoModel? ToModel(this ListTaskInfo? taskInfo)
    {
        if (taskInfo == null)
        {
            return null;
        }

        return new ListTaskInfoModel
        {
            ListId = taskInfo.ListId,
            Title = taskInfo.Title,
            Tasks = [.. taskInfo.Tasks.Select(t => t.ToModel())],
        };
    }

    public static TaskSummaryModel? ToModel(this TaskSummary taskSummary)
    {
        if (taskSummary == null)
        {
            return null;
        }

        return new TaskSummaryModel
        {
            Id = taskSummary.Id,
            Title = taskSummary.Title,
            Description = taskSummary.Description,
            CreationDateTime = taskSummary.CreationDateTime,
            DueDateTime = taskSummary.DueDateTime,
            TaskStatus = taskSummary.TaskStatus,
            Tags = [.. taskSummary.Tags.Select(t => t.ToModel())],
        };
    }

    public static TagModel? ToModel(this Tag tag)
    {
        if (tag == null)
        {
            return null;
        }

        return new TagModel
        {
            Id = tag.Id,
            Name = tag.Name,
        };
    }

    public static TaskDetailsWebApiModel? ToWebApiModel(this TaskDetails task)
    {
        if (task == null)
        {
            return null;
        }

        return new TaskDetailsWebApiModel
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            DueDateTime = task.DueDateTime,
            CreationDateTime = task.CreationDateTime,
            TaskStatus = task.TaskStatus,
            Comments = [.. task.Comments.Select(c => c.ToWebApiModel())],
            Tags = [.. task.Tags.Select(t => t.ToWebApiModel())],
        };
    }

    public static TagWebApiModel? ToWebApiModel(this Tag tag)
    {
        if (tag == null)
        {
            return null;
        }

        return new TagWebApiModel
        {
            Id = tag.Id,
            Name = tag.Name,
        };
    }

    public static CommentWebApiModel? ToWebApiModel(this Comment comment)
    {
        if (comment == null)
        {
            return null;
        }

        return new CommentWebApiModel
        {
            Id = comment.Id,
            Note = comment.Note,
            CreationDateTime = comment.CreationDateTime,
            LastEditDateTime = comment.LastEditDateTime,
        };
    }

    public static TaskDetails? ToDomain(this TaskDetailsWebApiModel? taskDetails)
    {
        if (taskDetails == null)
        {
            return null;
        }

        return new TaskDetails
        {
            Id = taskDetails.Id,
            Title = taskDetails.Title,
            Description = taskDetails.Description,
            DueDateTime = taskDetails.DueDateTime,
            CreationDateTime = taskDetails.CreationDateTime,
            TaskStatus = taskDetails.TaskStatus,
            Comments = [.. taskDetails.Comments.Select(c => c.ToDomain())],
            Tags = [.. taskDetails.Tags.Select(t => t.ToDomain())],
        };
    }

    public static Comment ToDomain(this CommentWebApiModel? comment)
    {
        if (comment == null)
        {
            return null;
        }

        return new Comment
        {
            Id = comment.Id,
            Note = comment.Note,
            CreationDateTime = comment.CreationDateTime,
            LastEditDateTime = comment.LastEditDateTime,
        };
    }

    public static TaskDetailsModel? ToModel(this TaskDetails taskDetails)
    {
        if (taskDetails == null)
        {
            return null;
        }

        return new TaskDetailsModel
        {
            Id = taskDetails.Id,
            Title = taskDetails.Title,
            Description = taskDetails.Description,
            CreationDateTime = taskDetails.CreationDateTime,
            DueDateTime = taskDetails.DueDateTime,
            TaskStatus = taskDetails.TaskStatus,
            Tags = [..taskDetails.Tags.Select(t => t.ToModel())],
            Comments = [..taskDetails.Comments.Select(c => c.ToModel())],
        };
    }

    public static CommentModel? ToModel(this Comment comment)
    {
        if (comment == null)
        {
            return null;
        }

        return new CommentModel
        {
            Id = comment.Id,
            Note = comment.Note,
            CreationDateTime = comment.CreationDateTime,
            LastEditDateTime = comment.LastEditDateTime,
        };
    }

    public static TaskDetails? ToDomain(this TaskDetailsModel model)
    {
        if (model == null)
        {
            return null;
        }

        return new TaskDetails
        {
            Id = model.Id,
            Description = model.Description,
            Title = model.Title,
            CreationDateTime = model.CreationDateTime,
            DueDateTime = model.DueDateTime,
            TaskStatus = model.TaskStatus,
            Comments = [.. model.Comments.Select(c => c.ToDomain())],
            Tags = [.. model.Tags.Select(t => t.ToDomain())],
        };
    }

    public static Comment? ToDomain(this CommentModel model)
    {
        if (model == null)
        {
            return null;
        }

        return new Comment
        {
            Id = model.Id,
            CreationDateTime = model.CreationDateTime,
            LastEditDateTime = model.LastEditDateTime,
            Note = model.Note,
        };
    }

    public static Tag? ToDomain(this TagModel model)
    {
        if (model == null)
        {
            return null;
        }

        return new Tag
        {
            Id = model.Id,
            Name = model.Name,
        };
    }

    public static TaskDetails? ToDomain(this ToDoListTaskEntity entity)
    {
        if (entity == null)
        {
            return null;
        }

        return new TaskDetails
        {
            Id = entity.Id,
            Title = entity.Title,
            Description = entity.Description,
            CreationDateTime = entity.CreationDateTime,
            DueDateTime = entity.DueDateTime,
            TaskStatus = entity.TaskStatus,
            Tags = [.. entity.Tags.Select(t => t.ToDomain())],
            Comments = [.. entity.Comments.Select(c => c.ToDomain())],
        };
    }

    public static Tag? ToDomain(this TagEntity entity)
    {
        if (entity == null)
        {
            return null;
        }

        return new Tag
        {
            Id = entity.Id,
            Name = entity.Name,
        };
    }

    public static Comment? ToDomain(this CommentEntity entity)
    {
        if (entity == null)
        {
            return null;
        }

        return new Comment
        {
            Id = entity.Id,
            Note = entity.Note,
            CreationDateTime = entity.CreationDateTime,
            LastEditDateTime = entity.LastEditDateTime,
        };
    }
}
