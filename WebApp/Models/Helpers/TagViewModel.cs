using WebApp.Models.Tags;

namespace WebApp.Models.Helpers;

public class TagViewModel
{
    public TagModel? Tag { get; set; }

    public bool HasDeleteButton { get; set; }

    public bool FiltersTasks { get; set; }
}
