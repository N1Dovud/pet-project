using WebApp.Business.ListTasks;
using WebApp.Business.Tags;
using WebApp.Common;

namespace WebApp.Services.TagService;

public interface ITagWebApiService
{
    Task<ResultWithData<List<Tag?>?>> GetAllTags();

    Task<ResultWithData<List<TaskSummary?>?>> GetTasksByTag(long tagId);

    Task<Result> AddTag(long taskId, string tagName);

    Task<Result> DeleteTag(long taskId, long tagId);
}
