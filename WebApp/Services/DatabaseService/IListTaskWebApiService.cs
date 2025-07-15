using WebApp.Business.ListTasks;
using WebApp.Models.ListTasks;

namespace WebApp.Services.DatabaseService;

public interface IListTaskWebApiService
{
    Task<ListTaskInfo?> GetListInfoAsync(long listId);
}
