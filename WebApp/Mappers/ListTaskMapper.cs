using WebApp.Business.Comments;
using WebApp.Business.ListTasks;
using WebApp.Business.Tags;
using WebApp.Models.Comments;
using WebApp.Models.ListTasks;
using WebApp.Models.Tags;

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
}
