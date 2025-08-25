using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Helpers;

public enum SearchFields
{
    Title,

    [Display(Name = "Creation Date")]
    CreationDate,

    [Display(Name = "Due Date")]
    DueDate,
}
