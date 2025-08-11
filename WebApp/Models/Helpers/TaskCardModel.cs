using WebApp.Business.ToDoLists;
using WebApp.Models.ListTasks;

namespace WebApp.Models.Helpers;

public class TaskCardModel
{
    public TaskSummaryModel? TaskSummary { get; set; }

    public bool IsOwner { get; set; }

    public bool HighlightOverdues { get; set; }

    public string? ReturnUrl { get; set; }
}
