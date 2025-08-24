namespace WebApp.Models.Comments;

internal class CommentModel
{
    public long Id { get; set; }

    public string Note { get; set; } = string.Empty;

    public DateTime CreationDateTime { get; set; } = DateTime.Now;

    public DateTime LastEditDateTime { get; set; } = DateTime.Now;
}
