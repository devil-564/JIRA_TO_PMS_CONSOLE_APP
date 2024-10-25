namespace JIRA_PMS_ConsoleAPP;

using Newtonsoft.Json;
using System;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json;
using System.Threading.Tasks;

internal class Program
{
    static public string GiveCorrectDateFormat(string date)
    {
        if (date != null)
        {
            DateTime obj = DateTime.Parse(date);
            string help = obj.ToString("yyyy-MM-dd");
            return help;
        }
        return "";
    }

    static public string GiveCorrectTimeEstimate(string time)
    {
        int help = Convert.ToInt32(time);

        Console.WriteLine(help);

        help = help / 3600;

        string correctedTime = help.ToString();
        correctedTime += " h";

        return correctedTime;
    }
    public static async Task Main(string[] args)
    {
        Class1 obj = new Class1();
        int BoardId = 0;
        bool BoardIdConfirmation = false;
        bool ProcessIsSprint = false;

        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
        };

        //Below Code Meaning - Basic Console App Interface for User Experience which provide basic information related to boards and project and user needs to select any one board or project from the available list using it's board id and program will return all the issues or task which are associated with that particular project.

        try
        {
            var responseAllBoard = await obj.GetResponse("https://irfan007lohar.atlassian.net/rest/agile/1.0/board/", null);
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
            return;
        }
        
        // Below Code Meaning - Now untill now user have inputted the board id and now from here i have first fetched the API which provide me list of issues available inside the board and then by deserializing it to an object I'll do the mapping to PMS JSON Schema Class which helps in Data Migration.

        var responseAllIssues = await obj.GetResponse($"https://irfan007lohar.atlassian.net/rest/agile/1.0/board/{BoardId}/issue", null);

        JiraTask jiraTask = JsonConvert.DeserializeObject<JiraTask>(responseAllIssues);

        var jiraTaskJson = System.Text.Json.JsonSerializer.Serialize(jiraTask, options);

        List<RequiredProjectJSON> projects = new List<RequiredProjectJSON>();

        ProjectJSON project = new();

        try
        {
            RequiredProjectJSON? ReqPJSON = null;

            foreach (var issue in jiraTask?.Issues)
            {
                ReqPJSON = new()
                {
                    id = issue.Fields.Project?.Id.ToString() ?? "N/A",
                    name = issue.Fields.Project?.Name ?? "N/A",
                    description = issue.Fields.Project?.Description ?? "N/A",
                    startDate = GiveCorrectDateFormat(issue.Fields.Created),
                    endDate = GiveCorrectDateFormat(issue.Fields.DueDate),
                    status = issue.Fields.Status?.Name ?? "N/A",
                    priority = issue.Fields.Priority?.Name ?? "N/A",
                    owner = new()
                    {
                        id = issue.Fields.Creator?.AccountId?.ToString(),
                        name = issue.Fields.Creator?.DisplayName,
                        email = issue.Fields.Creator?.EmailAddress,
                    },
                    tasks = new List<Task1>(),
                    resources = [],
                    customFields = { }
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
                    type = issue.Fields.IssueType?.Name,
                    status = issue.Fields.Status?.Name,
                    assignees = new List<Assignee1>(),
                    reporter = new()
                    {
                        id = issue.Fields.Reporter?.AccountId.ToString(),
                        name = issue.Fields.Reporter?.DisplayName,
                        email = issue.Fields.Reporter?.EmailAddress,
                    },
                    priority = issue.Fields.Priority?.Name,
                    startDate = GiveCorrectDateFormat(issue.Fields.CustomField_10015),
                    dueDate = GiveCorrectDateFormat(issue.Fields.DueDate),
                    subtasks = new List<SubTask1>(),
                    comments = new List<Comment1>(),
                    attachments = new List<Attachment1>(),
                    resolution = "N/A",
                    timeEstimate = GiveCorrectTimeEstimate(issue.Fields.TimeEstimate) ?? "N/A",
                    timelogs = issue.Fields.TimeTracking,
                    tags = issue.Fields.Labels,
                };

                // For Sub Task

                foreach (var subtask in issue.Fields.SubTasks)
                {

                    if (subtask != null)
                    {
                        SubTask1 stask = new SubTask1()
                        {
                            id = subtask.Id.ToString(),
                            title = subtask.Key,
                            status = subtask.Fields.Status?.Name
                        };
                        task.subtasks.Add(stask);
                    }

                }

                if (issue.Fields.Assignee != null)
                {
                    Assignee1 assignee = new Assignee1()
                    {
                        id = issue.Fields.Assignee.AccountId,
                        name = issue.Fields.Assignee.DisplayName,
                        email = issue.Fields.Assignee.EmailAddress
                    };
                    task.assignees.Add(assignee);
                }


                foreach (var comment in issue.Fields.Comment?.Comments)
                {
                    Comment1 comment1 = new Comment1()
                    {
                        id = comment.id.ToString(),
                        text = comment.Body,
                        timestamp = comment.Created,
                        author = new()
                        {
                            id = comment.Author.AccountId?.ToString(),
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
                        uploadDate = GiveCorrectDateFormat(attachment.Created.ToString()),
                        url = $"https://irfan007lohar.atlassian.net/rest/api/3/attachment/content/{attachment.Id}"
                    };

                    task.attachments.Add(attachment1);
                }

                ReqPJSON.tasks?.Add(task);
            }
            projects.Add(ReqPJSON);
            project.project = ReqPJSON;
        }

        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }

        string jsonString = System.Text.Json.JsonSerializer.Serialize(project, options);

        Console.WriteLine(jsonString);
    }
}