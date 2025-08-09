using WebApp.Business.ToDoLists;
using WebApp.Common;
using WebApp.Models;

namespace WebApp.Services.DatabaseService;

public interface IToDoListWebApiService
{
    Task<List<ToDoList?>?> GetToDoListsAsync();

    Task<Result> AddToDoListAsync(ToDoList? list);

    Task<Result> DeleteToDoListAsync(long listId);

    Task<Result> UpdateToDoListAsync(ToDoList? list);

    Task<ToDoList?> GetToDoListAsync(long listId);
}
