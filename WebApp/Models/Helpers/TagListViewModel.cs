using WebApp.Models.Tags;

namespace WebApp.Models.Helpers;

internal class TagListViewModel
{
    public TagListViewModel(IEnumerable<TagModel>? tags)
    {
        this.Tags = (tags ?? Enumerable.Empty<TagModel>()).ToList().AsReadOnly();
    }

    public IReadOnlyList<TagModel> Tags { get; }

    public string? ReturnUrl { get; set; }

    public long TaskId { get; set; }

    public bool IsOwner { get; set; }
}
