using WebApi.Business.ToDoLists;
using WebApi.Common;

namespace WebApi.Services.ListServices;

internal interface IToDoListService
{
    Task<ResultWithData<List<ToDoList?>?>> GetAllToDoListsAsync(long userId);

    Task<Result> AddToDoListAsync(ToDoList? list);

    Task<Result> DeleteToDoListAsync(long listId, long userId);

    Task<Result> UpdateToDoListAsync(ToDoList? list, long userId);

    Task<ResultWithData<ToDoList?>> GetToDoListAsync(long userId, long listId);
}
