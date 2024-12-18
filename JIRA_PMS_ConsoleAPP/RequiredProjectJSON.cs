﻿public class ProjectJSON
{
    public RequiredProjectJSON project { get; set; }
}

public class RequiredProjectJSON

{

    public string? id { get; set; } 

    public string? name { get; set; } 

    public string? description { get; set; } 

    public string? startDate { get; set; } 

    public string? endDate { get; set; } 

    public string? status { get; set; } 

    public string? priority { get; set; } 

    public Owner? owner { get; set; } 


    public Dictionary<string, string>? customFields { get; set; } = [];

    public List<Task1>? tasks { get; set; }

    public List<Resource>? resources { get; set; }

}

public class Owner

{

    public string? id { get; set; } 

    public string? name { get; set; } 

    public string? email { get; set; } 

}

public class Task1

{
    public string? id { get; set; } // Task unique identifier

    public string? title { get; set; } // Task title

    public string? description { get; set; } // Detailed description of the task

    public string? type { get; set; } // Task type (e.g., feature, bug)

    public string? status { get; set; } // Task status (e.g., open, in progress, completed)

    public List<Assignee1>? assignees { get; set; } // List of assigned users

    public Reporter? reporter { get; set; } // Task reporter

    public string? priority { get; set; } // Task priority (e.g., high, medium, low)

    public string? startDate { get; set; } // Task start date

    public string? dueDate { get; set; } // Task due date

    public string? timeEstimate { get; set; } // Estimated time

    public string? resolution { get; set; } // Resolution of the task

    public List<SubTask1>? subtasks { get; set; } // Nested subtasks

    public List<Comment1>? comments { get; set; } // Comments on the task

    public List<Attachment1>? attachments { get; set; } // Attachments linked to the task

    public Timelog? timelogs { get; set; } // Timelogs for the task

    public Dictionary<string, string>? customFields { get; set; } = [];// Custom fields for task-specific data
    public List<string>? tags { get; set; }
}

public class Assignee1

{

    public string? id { get; set; }

    public string? name { get; set; }

    public string? email { get; set; }

}

public class Reporter

{

    public string? id { get; set; }

    public string? name { get; set; }

    public string? email { get; set; }

}

public class SubTask1

{

    public string? id { get; set; }

    public string? title { get; set; }

    public string? status { get; set; }

}

public class Comment1

{

    public string? id { get; set; }

    public User? author { get; set; }

    public string? text { get; set; } // Comment content

    public DateTime? timestamp { get; set; } // Timestamp of comment creation

}

public class User

{

    public string? id { get; set; }

    public string? name { get; set; }

    public string? email { get; set; }

}

public class Attachment1

{

    public string? id { get; set; }

    public string? fileName { get; set; } // Name of the attached file

    public string? fileType { get; set; } // Type of file (e.g., .pdf, .docx)

    public double? fileSize { get; set; } // Size of the file in KB

    public string? uploadDate { get; set; } // When the file was uploaded

    public string? url { get; set; } // Download or view URL

}

public class Timelog

{

    public string? id { get; set; }

    public User? user { get; set; }

    public string? timeSpent { get; set; }

    public string? dateLogged { get; set; }

}

public class Resource

{

    public string? id { get; set; }

    public string? name { get; set; }

    public string? email { get; set; }

    public string? role { get; set; } // User's role in the project (e.g., manager, developer)

    public string? permissions { get; set; } // User's access level (e.g., read, write)

}
