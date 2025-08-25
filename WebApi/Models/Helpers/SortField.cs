using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Helpers;

public enum SortField
{
    Name,

    [Display(Name = "Due Date")]
    DueDate,
}
