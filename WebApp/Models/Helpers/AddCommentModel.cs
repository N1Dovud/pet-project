namespace WebApp.Models.Helpers;

public class AddCommentModel
{
    public string Note { get; set; }

    public long TaskId { get; set; }

    public string ReturnUrl { get; set; }
}
