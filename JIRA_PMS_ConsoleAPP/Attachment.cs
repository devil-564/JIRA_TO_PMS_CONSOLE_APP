namespace JIRA_PMS_ConsoleAPP
{
    public class Attachment
    {
        public int? Id { get; set; }
        public string? FileName { get; set; }
        public DateTime? Created { get; set; }
        public double? Size { get; set; } = 0;
        public string? MimeType { get; set; }
    }
}
