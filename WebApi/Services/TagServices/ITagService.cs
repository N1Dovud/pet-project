using System.Threading.Tasks;
using WebApi.Business.ListTasks;
using WebApi.Business.Tags;
using WebApi.Common;

namespace WebApi.Services.TagsServices;

public interface ITagService
{
    Task<ResultWithData<List<Tag?>?>> GetAllTags(long userId);

    Task<ResultWithData<List<TaskSummary?>?>> GetTasksByTag(long tagId, long userId);

    Task<Result> AddTag(long userId, string tagName, long taskId);

    Task<Result> DeleteTag(long userId, long tagId, long taskId);
}
