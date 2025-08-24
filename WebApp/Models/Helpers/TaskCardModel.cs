using WebApp.Models.ListTasks;

namespace WebApp.Models.Helpers;

internal class TaskCardModel
{
    public TaskSummaryModel? TaskSummary { get; set; }

    public bool IsOwner { get; set; }

    public bool HighlightOverdues { get; set; }

    public string? ReturnUrl { get; set; }
}
