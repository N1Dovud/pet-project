using WebApp.Models.Tags;

namespace WebApp.Models.Helpers;

public class TagListViewModel
{
    public List<TagModel> Tags { get; set; } = [];

    public string? ReturnUrl { get; set; }

    public long TaskId { get; set; }

    public bool IsOwner { get; set; }
}
