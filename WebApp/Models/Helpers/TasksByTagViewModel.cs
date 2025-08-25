using WebApp.Models.ListTasks;
using WebApp.Models.Tags;

namespace WebApp.Models.Helpers;

public class TasksByTagViewModel
{
    public List<TaskSummaryModel?> TaskSummaries { get; set; } = [];

    public TagModel? Tag { get; set; }
}
