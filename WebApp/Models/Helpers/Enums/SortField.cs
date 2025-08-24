using System.ComponentModel.DataAnnotations;

namespace WebApp.Models.Helpers.Enums;

internal enum SortField
{
    Name,

    [Display(Name = "Due Date")]
    DueDate,
}
