namespace WebApi.Models.ListTasks
{
    public class ListTaskInfoModel
    {
        public long ListId { get; set; }

        public string Title { get; set; } = string.Empty;

        public List<TaskSummaryModel> Tasks { get; set; } = [];
    }
}
