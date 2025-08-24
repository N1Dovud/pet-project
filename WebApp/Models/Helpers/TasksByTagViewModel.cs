using WebApp.Models.Comments;
using WebApp.Models.ListTasks;
using WebApp.Models.Tags;

namespace WebApp.Models.Helpers;

internal class TasksByTagViewModel
{
    public TasksByTagViewModel(IEnumerable<TaskSummaryModel?> summaries)
    {
        this.TaskSummaries = summaries.ToList().AsReadOnly();
    }

    public IReadOnlyList<TaskSummaryModel?>? TaskSummaries { get; } =[];

    public TagModel? Tag { get; set; }
}
