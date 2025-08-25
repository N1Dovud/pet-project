using WebApp.Business.Comments;
using WebApp.Business.ListTasks;
using WebApp.Business.Tags;
using WebApp.Models.Comments;
using WebApp.Models.ListTasks;
using WebApp.Models.Tags;

namespace WebApp.Mappers;

internal static class ListTaskMapper
{
    public static TaskDetails ToDomain(this TaskDetailsModel model)
    {
        ArgumentNullException.ThrowIfNull(model);

        return new TaskDetails
        {
            Id = model.Id,
            Description = model.Description,
            Title = model.Title,
            CreationDateTime = model.CreationDateTime,
            DueDateTime = model.DueDateTime,
            Tags = [.. model.Tags.Select(t => t.ToDomain())],
            Comments = [.. model.Comments.Select(c => c.ToDomain())],
            TaskStatus = model.TaskStatus,
        };
    }

    public static Comment ToDomain(this CommentModel model)
    {
        ArgumentNullException.ThrowIfNull(model);

        return new Comment
        {
            Id = model.Id,
            CreationDateTime = model.CreationDateTime,
            LastEditDateTime = model.LastEditDateTime,
            Note = model.Note,
        };
    }

    public static Tag ToDomain(this TagModel model)
    {
        ArgumentNullException.ThrowIfNull(model);

        return new Tag
        {
            Id = model.Id,
            Name = model.Name,
        };
    }

    public static TaskDetails ToDomain(this TaskDetailsWebApiModel? taskDetails)
    {
        ArgumentNullException.ThrowIfNull(taskDetails);

        return new TaskDetails
        {
            Id = taskDetails.Id,
            Title = taskDetails.Title,
            Description = taskDetails.Description,
            DueDateTime = taskDetails.DueDateTime,
            CreationDateTime = taskDetails.CreationDateTime,
            TaskStatus = taskDetails.TaskStatus,
            Tags = [.. taskDetails.Tags.Select(t => t.ToDomain())],
            Comments = [.. taskDetails.Comments.Select(c => c.ToDomain())],
        };
    }

    public static Comment ToDomain(this CommentWebApiModel? comment)
    {
        ArgumentNullException.ThrowIfNull(comment);

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

        return new ListTaskInfo
        {
            ListId = taskInfo.ListId,
            Title = taskInfo.Title,
            Tasks = [.. taskInfo.Tasks.Select(t => t.ToDomain())],
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
            Tags = [.. taskSummary.Tags.Select(t => t.ToDomain())],
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

    public static ListTaskInfoModel ToModel(this ListTaskInfo taskInfo)
    {
        ArgumentNullException.ThrowIfNull(taskInfo);

        return new ListTaskInfoModel
        {
            ListId = taskInfo.ListId,
            Title = taskInfo.Title,
            Tasks = [.. taskInfo.Tasks.Select(t => t.ToModel())],
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
            Tags = [.. taskSummary.Tags.Select(t => t.ToModel())],
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

    public static TaskDetailsModel ToModel(this TaskDetails taskDetails)
    {
        ArgumentNullException.ThrowIfNull(taskDetails);

        return new TaskDetailsModel
        {
            Id = taskDetails.Id,
            Title = taskDetails.Title,
            Description = taskDetails.Description,
            CreationDateTime = taskDetails.CreationDateTime,
            DueDateTime = taskDetails.DueDateTime,
            TaskStatus = taskDetails.TaskStatus,
            Tags = [.. taskDetails.Tags.Select(t => t.ToModel())],
            Comments = [.. taskDetails.Comments.Select(c => c.ToModel())],
        };
    }

    public static CommentModel ToModel(this Comment comment)
    {
        ArgumentNullException.ThrowIfNull(comment);

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

    public static TagWebApiModel ToWebApiModel(this Tag tag)
    {
        ArgumentNullException.ThrowIfNull(tag);

        return new TagWebApiModel
        {
            Id = tag.Id,
            Name = tag.Name,
        };
    }

    public static CommentWebApiModel ToWebApiModel(this Comment comment)
    {
        ArgumentNullException.ThrowIfNull(comment);

        return new CommentWebApiModel
        {
            Id = comment.Id,
            Note = comment.Note,
            CreationDateTime = comment.CreationDateTime,
            LastEditDateTime = comment.LastEditDateTime,
        };
    }
}
