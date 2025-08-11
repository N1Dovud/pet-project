using WebApp.Business.Tags;
using WebApp.Common;

namespace WebApp.Services.TagService;

public interface ITagWebApiService
{
    Task<ResultWithData<List<Tag?>?>> GetAllTags();
}
