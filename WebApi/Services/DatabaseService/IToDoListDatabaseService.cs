using WebApi.Business;

namespace WebApi.Services.DatabaseService;

public interface IToDoListDatabaseService
{
    Task<List<ToDoList>> GetAllToDoListsAsync(long userId);

    Task<bool> AddToDoListAsync(ToDoList list);
}
