using WebApp.Models.Comments;

namespace WebApp.Models.Helpers;

internal class CommentViewModel
{
    public IReadOnlyList<CommentModel> Comments { get; set; } =[];

    public string? ReturnUrl { get; set; }

    public long TaskId { get; set; }

    public bool IsOwner { get; set; }
}
