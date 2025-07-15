using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WebApp.Models.Enums;
using WebApp.Models.Tags;
using WebApp.Models.Enums;

namespace WebApp.Models.ListTasks;

public class TaskSummaryModel
{
    public long Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public DateTime CreationDateTime { get; set; }

    public DateTime DueDateTime { get; set; }

    public ToDoListTaskStatus TaskStatus { get; set; }

    public List<TagModel> Tags { get; set; } = [];
}
