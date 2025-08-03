using System.ComponentModel.DataAnnotations;

namespace WebApp.Models.Enums;

public enum ToDoListTaskStatus
{
    [Display(Name = "Not Started")]
    NotStarted,

    [Display(Name = "In Progress")]
    InProgress,

    [Display(Name = "Completed")]
    Completed,
}
