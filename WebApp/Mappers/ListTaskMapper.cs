using WebApp.Business.Comments;
using WebApp.Business.ListTasks;
using WebApp.Business.Tags;
using WebApp.Models.Comments;
using WebApp.Models.ListTasks;
using WebApp.Models.Tags;

namespace WebApp.Mappers;

internal static class ListTaskMapper
{
    public static TaskDetails? ToDomain(this TaskDetailsModel model)
    {
        if (model == null)
        {
            return null;
        }

        return new TaskDetails(
            model.Tags.Select(t => t?.ToDomain()),
            model.Comments.Select(c => c.ToDomain()))
        {
            Id = model.Id,
            Description = model.Description,
            Title = model.Title,
            CreationDateTime = model.CreationDateTime,
            DueDateTime = model.DueDateTime,
            TaskStatus = model.TaskStatus,
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

    public static TaskDetails? ToDomain(this TaskDetailsWebApiModel? taskDetails)
    {
        if (taskDetails == null)
        {
            return null;
        }

        return new TaskDetails(
            taskDetails.Tags.Select(t => t.ToDomain()),
            taskDetails.Comments.Select(c => c.ToDomain()))
        {
            Id = taskDetails.Id,
            Title = taskDetails.Title,
            Description = taskDetails.Description,
            DueDateTime = taskDetails.DueDateTime,
            CreationDateTime = taskDetails.CreationDateTime,
            TaskStatus = taskDetails.TaskStatus,
        };
    }

    public static Comment? ToDomain(this CommentWebApiModel? comment)
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

    public static ListTaskInfo? ToDomain(this ListTaskInfoWebApiModel? taskInfo)
    {
        if (taskInfo == null)
        {
            return null;
        }

        return new ListTaskInfo(taskInfo.Tasks.Select(t => t.ToDomain()))
        {
            ListId = taskInfo.ListId,
            Title = taskInfo.Title,
        };
    }

    public static TaskSummary ToDomain(this TaskSummaryWebApiModel taskSummary)
    {
        return new TaskSummary(taskSummary.Tags.Select(t => t.ToDomain()))
        {
            Id = taskSummary.Id,
            Title = taskSummary.Title,
            Description = taskSummary.Description,
            CreationDateTime = taskSummary.CreationDateTime,
            DueDateTime = taskSummary.DueDateTime,
            TaskStatus = taskSummary.TaskStatus,
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

        return new ListTaskInfoModel(taskInfo.Tasks.Select(t => t.ToModel()) ?? [])
        {
            ListId = taskInfo.ListId,
            Title = taskInfo.Title,
        };
    }

    public static TaskSummaryModel? ToModel(this TaskSummary taskSummary)
    {
        if (taskSummary == null)
        {
            return null;
        }

        return new TaskSummaryModel(taskSummary.Tags.Select(t => t.ToModel()))
        {
            Id = taskSummary.Id,
            Title = taskSummary.Title,
            Description = taskSummary.Description,
            CreationDateTime = taskSummary.CreationDateTime,
            DueDateTime = taskSummary.DueDateTime,
            TaskStatus = taskSummary.TaskStatus,
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

    public static TaskDetailsModel? ToModel(this TaskDetails taskDetails)
    {
        if (taskDetails == null)
        {
            return null;
        }

        return new TaskDetailsModel(
            taskDetails.Tags.Select(t => t.ToModel()),
            taskDetails.Comments.Select(c => c.ToModel()))
        {
            Id = taskDetails.Id,
            Title = taskDetails.Title,
            Description = taskDetails.Description,
            CreationDateTime = taskDetails.CreationDateTime,
            DueDateTime = taskDetails.DueDateTime,
            TaskStatus = taskDetails.TaskStatus,
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

    public static TaskDetailsWebApiModel? ToWebApiModel(this TaskDetails task)
    {
        if (task == null)
        {
            return null;
        }

        return new TaskDetailsWebApiModel(
            task.Tags.Select(t => t.ToWebApiModel()) ?? [],
            task.Comments.Select(c => c.ToWebApiModel()) ?? [])
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            DueDateTime = task.DueDateTime,
            CreationDateTime = task.CreationDateTime,
            TaskStatus = task.TaskStatus,
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
}
