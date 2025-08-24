using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Helpers;

internal enum SortField
{
    Name,

    [Display(Name = "Due Date")]
    DueDate,
}
