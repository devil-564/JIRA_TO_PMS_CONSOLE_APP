public class Lead
{
    public string DisplayName { get; set; }
    public bool Active { get; set; }  // Updated to 'bool' since the JSON shows 'active' is a boolean
}

public class JiraProject
{
    public string Key { get; set; }
    public string Description { get; set; }
    public Lead Lead { get; set; }
    public string ProjectTypeKey { get; set; }
    public string AssigneeType { get; set; }
    public string Name { get; set; }
}
