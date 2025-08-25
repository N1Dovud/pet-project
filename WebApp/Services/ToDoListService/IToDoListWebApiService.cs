using WebApp.Business.ToDoLists;
using WebApp.Common;

namespace WebApp.Services.ToDoListService;

public interface IToDoListWebApiService
{
    Task<ResultWithData<List<ToDoList?>?>> GetToDoListsAsync();

    Task<Result> AddToDoListAsync(ToDoList? list);

    Task<Result> DeleteToDoListAsync(long listId);

    Task<Result> UpdateToDoListAsync(ToDoList? list);

    Task<ResultWithData<ToDoList?>> GetToDoListAsync(long listId);
}
