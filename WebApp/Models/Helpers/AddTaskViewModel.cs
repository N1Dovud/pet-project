using WebApp.Business.ListTasks;
using WebApp.Models.ListTasks;

namespace WebApp.Models.Helpers;

public class AddTaskViewModel
{
    public long listId { get; set; }

    public TaskDetailsModel taskDetails { get; set; }
}
