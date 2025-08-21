namespace WebApi.Models.Helpers;

public class AddTagModel
{
    public required string TagName { get; set; }

    public long TaskId { get; set; }
}
