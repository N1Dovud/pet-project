using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.Models.Tags;

public class TagModel
{
    public long Id { get; set; }

    public string Name { get; set; } = string.Empty;
}
