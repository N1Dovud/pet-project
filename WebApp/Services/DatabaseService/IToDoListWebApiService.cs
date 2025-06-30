using WebApp.Business;
using WebApp.Models;

namespace WebApp.Services.DatabaseService;

public interface IToDoListWebApiService
{
    Task<List<ToDoList?>?> GetToDoLists();

    Task<Result> AddToDoListAsync(ToDoList? list);

    Task<Result> DeleteToDoListAsync(long listId);
}
