using WebApp.Business.ListTasks;
using WebApp.Models.ListTasks;

namespace WebApp.Models.Helpers;

public class AssignedTasksModel
{
    public StatusFilter Filter { get; set; }

    public List<TaskSummaryModel?> Tasks { get; set; }
}
