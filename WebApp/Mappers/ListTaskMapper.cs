using WebApp.Business.ListTasks;
using WebApp.Business.Tags;
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
}
