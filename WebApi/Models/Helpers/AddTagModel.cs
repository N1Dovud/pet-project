namespace WebApi.Models.Helpers;

public class AddTagModel
{
    required public string TagName { get; set; }

    public long TaskId { get; set; }
}
