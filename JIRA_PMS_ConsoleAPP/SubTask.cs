namespace JIRA_PMS_ConsoleAPP
{
    public class SubTaskFieldsStatus
    {
        public string? Name { get; set; }
    }

    public class SubTaskFields
    {
        public string? Summary { get; set; }
        public SubTaskFieldsStatus? Status { get; set; }
    }

    public class SubTask
    {
        public int? Id { get; set; }

        public string? Key { get; set; }

        public SubTaskFields Fields { get; set; }
    }
}
