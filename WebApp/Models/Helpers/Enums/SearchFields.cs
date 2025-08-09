using System.ComponentModel.DataAnnotations;

namespace WebApp.Models.Helpers.Enums;

public enum SearchFields
{
    Title,

    [Display(Name = "Creation Date")]
    CreationDate,

    [Display(Name = "Due Date")]
    DueDate,
}
