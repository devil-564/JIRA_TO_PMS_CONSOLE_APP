namespace JIRA_PMS_ConsoleAPP
{
    public class Field {
    
        public string? Description { get; set; }
        public Comment? Comment { get; set; }
        public List<SubTask>? SubTasks { get; set; }
        public List<Attachment>? Attachment { get; set; }
        public Users? Creator { get; set; }
        public Reporter? Reporter { get; set; }
        public Assignee? Assignee { get; set; }
        public IssueType? IssueType { get; set; }
        public Status? Status { get; set; }

        public string? Created { get; set; }
        public string? DueDate { get; set; }

        public List<string>? Labels { get; set; }

        public Priority? Priority { get; set; }

        public Resolution1? Resolution { get; set; }

        public Project? Project { get; set; }

        public Timelog? TimeTracking { get; set; }

        public string? CustomField_10015 { get; set; }

        public string? TimeEstimate { get; set; }
    }
    
    public class Issue
    {
        public int Id { get; set; }
        public string? Key { get; set; }
        public Field Fields { get; set; }
    }

    public class JiraTask
    {
        public List<Issue> Issues { get; set; }
    }
}
