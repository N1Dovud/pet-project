using System.ComponentModel.DataAnnotations;

namespace WebApp.Models.Helpers;

public enum SortField
{
    Name,

    [Display(Name = "Due Date")]
    DueDate,
}
