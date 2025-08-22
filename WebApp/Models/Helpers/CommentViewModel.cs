using WebApp.Models.Comments;

namespace WebApp.Models.Helpers;

public class CommentViewModel
{
    public List<CommentModel> Comments { get; set; } = new List<CommentModel>();
}
