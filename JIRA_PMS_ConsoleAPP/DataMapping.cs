//using JIRA_PMS_ConsoleAPP;

//public RequiredProjectJSON MapJiraToProject(JiraTask jiraTask)
//{
//    // Initialize the project JSON object
//    RequiredProjectJSON project = new RequiredProjectJSON
//    {
//        id = Guid.NewGuid().ToString(),  // Assuming project ID is generated
//        name = "Project from Jira",  // Set a project name
//        description = jiraTask.Issues.FirstOrDefault()?.Fields.Description,  // Description from the first issue
//        startDate = jiraTask.Issues.FirstOrDefault()?.Fields.Created,
//        endDate = jiraTask.Issues.FirstOrDefault()?.Fields.DueDate,
//        status = jiraTask.Issues.FirstOrDefault()?.Fields.Status.Name,  // Map Jira status
//        priority = jiraTask.Issues.FirstOrDefault()?.Fields.Priority.Name,
//        owner = new Owner
//        {
//            id = jiraTask.Issues.FirstOrDefault()?.Fields.Creator.Name,
//            name = jiraTask.Issues.FirstOrDefault()?.Fields.Creator.Name,
//            email = "" // Assuming no email from Jira, you can fill this if available
//        },
//        tags = jiraTask.Issues.FirstOrDefault()?.Fields.Labels,
//        tasks = new List<Task>(),
//        resources = new List<Resource>()
//    };

//    //// Map each Jira issue to a project task
//    //foreach (var issue in jiraTask.Issues)
//    //{
//    //    Task task = new Task
//    //    {
//    //        id = issue.Key,  // Map Jira issue key to task ID
//    //        title = issue.Fields.IssueType.Name,  // Map Jira issue type to task title
//    //        description = issue.Fields.Description,  // Map issue description
//    //        type = issue.Fields.IssueType.Name,  // Map task type
//    //        status = issue.Fields.Status.Name,  // Map task status
//    //        priority = issue.Fields.Priority.Name,  // Map task priority
//    //        startDate = issue.Fields.Created,
//    //        dueDate = issue.Fields.DueDate,
//    //        assignees = issue.Fields.Assignees.Select(assignee => new Assignee
//    //        {
//    //            id = assignee.Name,
//    //            name = assignee.Name
//    //        }).ToList(),
//    //        comments = issue.Fields.Comment.Comments?.Select(c => new Comment
//    //        {
//    //            id = Guid.NewGuid().ToString(),
//    //            author = new User
//    //            {
//    //                name = c.Author.DisplayName
//    //            },
//    //            text = c.Body,
//    //            timestamp = DateTime.Parse(c.Created)
//    //        }).ToList(),
//    //        subtasks = issue.Fields.SubTask.Select(subTask => new Subtask
//    //        {
//    //            id = subTask.Key,
//    //            title = subTask.Fields.Description,
//    //            status = subTask.Fields.Status.Name
//    //        }).ToList(),
//    //        attachments = issue.Fields.Attachment.Select(att => new Attachment
//    //        {
//    //            id = Guid.NewGuid().ToString(),
//    //            fileName = att.Filename,
//    //            fileType = att.MimeType,
//    //            fileSize = att.Size / 1024.0, // Assuming size is in bytes, converting to KB
//    //            uploadDate = DateTime.Parse(att.Created)
//    //        }).ToList()
//    //    };

//    //    project.tasks.Add(task);
//    //}

//    //return project;
//}
