using System.ComponentModel.DataAnnotations;

namespace WebApp.Models.Helpers.Enums;

public enum StatusFilter
{
    [Display(Name = "Not Started")]
    NotStarted,

    [Display(Name = "In Progress")]
    InProgress,

    Active,

    [Display(Name = "Completed")]
    Completed,
}
