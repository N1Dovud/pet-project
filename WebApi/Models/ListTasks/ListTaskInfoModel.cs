namespace WebApi.Models.ListTasks
{
    internal class ListTaskInfoModel
    {
        public long ListId { get; set; }

        public string Title { get; set; } = string.Empty;

        public IReadOnlyList<TaskSummaryModel> Tasks { get; } =[];
    }
}
