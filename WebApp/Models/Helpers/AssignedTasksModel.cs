using WebApp.Models.Helpers.Enums;
using WebApp.Models.ListTasks;

namespace WebApp.Models.Helpers;

internal class AssignedTasksModel
{
    public AssignedTasksModel(IEnumerable<TaskSummaryModel?> tasks)
    {
        this.Tasks = tasks.ToList().AsReadOnly();
    }

    public StatusFilter Filter { get; set; }

    public SortField? SortBy { get; set; }

    public bool Descending { get; set; }

    public IReadOnlyList<TaskSummaryModel?> Tasks { get; } =[];
}
