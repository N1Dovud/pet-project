using WebApi.Business.ToDoLists;

namespace WebApi.Services.DatabaseService;

public interface IToDoListDatabaseService
{
    Task<List<ToDoList>> GetAllToDoListsAsync(long userId);

    Task<Result> AddToDoListAsync(ToDoList? list);

    Task<Result> DeleteToDoListAsync(long listId, long userId);

    Task<Result> UpdateToDoListAsync(ToDoList? list, long userId);

    Task<ToDoList?> GetToDoListAsync(long userId, long listId);
}
