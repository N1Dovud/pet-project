namespace WebApp.Models.ListTasks
{
    internal class ListTaskInfoModel
    {
        public ListTaskInfoModel(IEnumerable<TaskSummaryModel> tasks)
        {
            this.Tasks = tasks.ToList().AsReadOnly();
        }

        public long ListId { get; set; }

        public string Title { get; set; } = string.Empty;

        public IReadOnlyList<TaskSummaryModel> Tasks { get; }
    }
}
