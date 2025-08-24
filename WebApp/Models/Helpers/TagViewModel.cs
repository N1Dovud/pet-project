using WebApp.Models.Tags;

namespace WebApp.Models.Helpers;

internal class TagViewModel
{
    public TagModel? Tag { get; set; }

    public bool HasDeleteButton { get; set; }

    public bool FiltersTasks { get; set; }

    public bool DisplayTag { get; set; }

    public long TaskId { get; set; }

    public string? ReturnUrl { get; set; }
}
