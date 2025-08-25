namespace WebApi.Business.Comments;

public class Comment
{
    public long Id { get; set; }

    public string Note { get; set; } = string.Empty;

    public DateTime CreationDateTime { get; set; } = DateTime.Now;

    public DateTime LastEditDateTime { get; set; } = DateTime.Now;
}
