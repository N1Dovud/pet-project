using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Helpers;

internal enum StatusFilter
{
    [Display(Name = "Not Started")]
    NotStarted,

    [Display(Name = "In Progress")]
    InProgress,

    Active,

    [Display(Name = "Completed")]
    Completed,
}
