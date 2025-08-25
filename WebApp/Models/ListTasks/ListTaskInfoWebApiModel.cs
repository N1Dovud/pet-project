namespace WebApp.Models.ListTasks
{
    public class ListTaskInfoWebApiModel
    {
        public long ListId { get; set; }

        public string Title { get; set; } = string.Empty;

        public List<TaskSummaryWebApiModel> Tasks { get; set; } = [];
    }
}
