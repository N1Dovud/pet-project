using WebApp.Business;
using WebApp.Models;

namespace WebApp.Services.DatabaseService;

public interface IToDoListWebApiService
{
    Task<List<ToDoList?>?> GetToDoLists();
}
