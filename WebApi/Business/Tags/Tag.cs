using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Business.Tags;

public class Tag
{
    public long Id { get; set; }

    public string Name { get; set; } = string.Empty;
}
