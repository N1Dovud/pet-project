using System.ComponentModel.DataAnnotations;

namespace WebApp.Models.Helpers.Enums;

public enum SortField
{
    Name,

    [Display(Name = "Due Date")]
    DueDate,
}
