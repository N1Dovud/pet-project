namespace WebApi.Models.Helpers;

internal class AddTagModel
{
    required public string TagName { get; set; }

    public long TaskId { get; set; }
}
