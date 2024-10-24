namespace JIRA_PMS_ConsoleAPP;

using Newtonsoft.Json;
using System;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json;
using System.Threading.Tasks;

internal class Program
{
    public static async Task Main(string[] args)
    {
        Class1 obj = new Class1();
        int BoardId = 0;
        bool BoardIdConfirmation = false;

        try
        {
            var responseAllBoard = await obj.MakeGitLabAPIRequest("https://irfan007lohar.atlassian.net/rest/agile/1.0/board/", null);
            JiraBoardResponse Board = JsonConvert.DeserializeObject<JiraBoardResponse>(responseAllBoard);

            Console.WriteLine("\t\t\t\t\t WELCOME TO JIRA - PMS DATA MIGRATION\n");
            Console.WriteLine("-> List of Boards present in your JIRA Account\n");

            // Showing Available Boards
            foreach (var BoardValue in Board.Values)
            {
                Console.WriteLine($"\t\tId = {BoardValue.Id} | Board Name - {BoardValue.Name} | Project Name - {BoardValue.Location.ProjectName}");
            }

            Console.WriteLine("\n-> Enter Board Id in which you want to do the Data Migration \n");

            // Selecting Available Boards
            while (true)
            {
                BoardId = Convert.ToInt32(Console.ReadLine());

                foreach (var BoardValue in Board.Values)
                {
                    if (BoardValue.Id == BoardId)
                    {
                        Console.WriteLine("-> Board Selected Successfully");
                        BoardIdConfirmation = true;
                        break;
                    }
                }

                if (!BoardIdConfirmation)
                {
                    Console.WriteLine("-> Please Enter Board Id which is Available");
                }
                else
                {
                    break;
                }
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return; // Exit if there's an exception
        }

        bool ProcessIsSprint = false;

        try
        {
            var responseAllSprint = await obj.MakeGitLabAPIRequest($"https://irfan007lohar.atlassian.net/rest/agile/1.0/board/{BoardId}/sprint/1/issue", null);

            RequiredProjectJSON JiraProcess = JsonConvert.DeserializeObject<RequiredProjectJSON>(responseAllSprint);

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }

        Console.WriteLine("Start");

        var responseAllIssues = await obj.MakeGitLabAPIRequest($"https://irfan007lohar.atlassian.net/rest/agile/1.0/board/{BoardId}/sprint/1/issue", null);
        

        JiraTask jiraTask = JsonConvert.DeserializeObject<JiraTask>(responseAllIssues);

        //foreach (var i in jiraTask.Issues) 
        //{
        //        foreach (var j in i.Fields.Attachment)
        //        {
        //            Console.WriteLine(j.Size);
        //        }
        //}

        List<RequiredProjectJSON> projects = new List<RequiredProjectJSON>();
        Project project = new Project();
        try
        {

            // For Project
            RequiredProjectJSON ReqPJSON = null;

            foreach (var issue in jiraTask.Issues)
            {
                ReqPJSON = new()
                {
                    id = issue.Fields.Project.Id.ToString(),
                    name = issue.Fields.Project.Name,
                    description = issue.Fields.Project.Description,
                    startDate = issue.Fields.Created,
                    endDate = issue.Fields.DueDate,
                    status = issue.Fields.Status.Name,
                    priority = issue.Fields.Priority.Name,
                    owner = new()
                    {
                        id = issue.Fields.Creator.AccountId.ToString(),
                        name = issue.Fields.Creator.DisplayName,
                        email = issue.Fields.Creator.EmailAddress,
                    },
                    tags = issue.Fields.Labels,
                    tasks = new List<Task1>(),
                };
            }

            // For Task

            foreach (var issue in jiraTask.Issues)
            {
                Task1 task = new Task1()
                {
                    id = issue.Id.ToString(),
                    title = issue.Key,
                    description = issue.Fields.Description,
                    type = issue.Fields.IssueType.Name,
                    status = issue.Fields.Status.Name,
                    assignees = new List<Assignee>(),
                    reporter = new()
                    {
                        id = issue.Fields.Reporter.AccountId.ToString(),
                        name = issue.Fields.Reporter.DisplayName,
                        email = issue.Fields.Reporter.EmailAddress,
                    },
                    priority = issue.Fields.Priority.Name,
                    startDate = issue.Fields.Created,
                    dueDate = issue.Fields.DueDate,
                    //resolution = issue.Fields.Resolution.Name,
                    subtasks = new List<SubTask1>(),
                    comments = new List<Comment1>(),
                    attachments = new List<Attachment1>()
                };

                // For Sub Task

                    if (issue.Fields.SubTask != null)
                    {
                        foreach (var subtask in issue.Fields.SubTask)
                        {
                            SubTask1 stask = new SubTask1()
                            {
                                id = subtask.Id.ToString(),
                                title = subtask.Key,
                                status = subtask.Fields.Status.Name,
                                assignees = new List<Assignee>()
                            };

                            task.subtasks.Add(stask);
                        }
                    }

                    if (issue.Fields.Assignees != null)
                    {
                        Assignee assignee = new Assignee()
                        {
                            id = issue.Fields.Assignees.AccountId.ToString(),
                            name = issue.Fields.Assignees.DisplayName,
                            email = issue.Fields.Assignees.EmailAddress
                        };

                        task.assignees.Add(assignee);
                    }

                foreach (var comment in issue.Fields.Comment.Comments)
                {
                    Comment1 comment1 = new Comment1()
                    {
                        id = comment.id.ToString(),
                        text = comment.Body,
                        timestamp = comment.Created,
                        author = new()
                        {
                            id = comment.Author.AccountId.ToString(),
                            name = comment.Author.DisplayName,
                            email = comment.Author.EmailAddress
                        }
                    };

                    task.comments.Add(comment1);
                }

                foreach (var attachment in issue.Fields.Attachment)
                {
                    Attachment1 attachment1 = new Attachment1()
                    {
                        id = attachment.Id.ToString(),
                        fileName = attachment.FileName,
                        fileType = attachment.MimeType,
                        fileSize = (attachment.Size / 1024),
                        uploadDate = attachment.Created,
                        url = $"https://irfan007lohar.atlassian.net/rest/api/3/attachment/content/{attachment.Id}"
                    };

                    task.attachments.Add(attachment1);
                }

                ReqPJSON.tasks.Add(task);
            }
            projects.Add(ReqPJSON);
            project = ReqPJSON;
        }

        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }


        Console.WriteLine("JSON Object");

        var options = new JsonSerializerOptions
        {
            WriteIndented = true // for pretty-printing the JSON output
        };

        // Serialize the first project object in the list to JSON
        string jsonString = System.Text.Json.JsonSerializer.Serialize(projects[0], options);

        Console.WriteLine(jsonString);

    }
}  