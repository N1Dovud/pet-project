using WebApp.Models.Comments;

namespace WebApp.Models.Helpers;

public class CommentViewModel
{
    public List<CommentModel> Comments { get; set; } = [];

    public string? ReturnUrl { get; set; }

    public long TaskId { get; set; }

    public bool IsOwner { get; set; }
}
