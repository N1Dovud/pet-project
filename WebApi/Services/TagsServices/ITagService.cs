using WebApi.Business.Tags;
using WebApi.Common;

namespace WebApi.Services.TagsServices;

public interface ITagService
{
    Task<ResultWithData<List<Tag?>?>> GetAllTags(long userId);
}
