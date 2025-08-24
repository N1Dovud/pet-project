using WebApp.Business.ToDoLists;
using WebApp.Common;

namespace WebApp.Services.ToDoListService;

internal interface IToDoListWebApiService
{
    Task<List<ToDoList?>?> GetToDoListsAsync();

    Task<Result> AddToDoListAsync(ToDoList? list);

    Task<Result> DeleteToDoListAsync(long listId);

    Task<Result> UpdateToDoListAsync(ToDoList? list);

    Task<ToDoList?> GetToDoListAsync(long listId);
}
