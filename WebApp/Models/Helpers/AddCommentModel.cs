namespace WebApp.Models.Helpers;

internal class AddCommentModel
{
    public string? Note { get; set; }

    public long TaskId { get; set; }

    public string? ReturnUrl { get; set; }
}
