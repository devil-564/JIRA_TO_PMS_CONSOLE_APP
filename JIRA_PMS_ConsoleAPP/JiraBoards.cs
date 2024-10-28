namespace JIRA_PMS_ConsoleAPP
{
    public class Location
    {
        public int ProjectId { get; set; }
        public string DisplayName { get; set; }
        public string ProjectName { get; set; }
        public string ProjectKey { get; set; }
        public string ProjectTypeKey { get; set; }
        public string AvatarURI { get; set; }
        public string Name { get; set; }
    }

    public class BoardValue
    {
        public int Id { get; set; }
        public string Self { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public Location Location { get; set; }
        public bool IsPrivate { get; set; }
    }

    public class JiraBoardResponse
    {
        public int MaxResults { get; set; }
        public int StartAt { get; set; }
        public int Total { get; set; }
        public bool IsLast { get; set; }
        public List<BoardValue> Values { get; set; } 
    }
}
