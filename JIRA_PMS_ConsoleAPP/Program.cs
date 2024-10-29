namespace JIRA_PMS_ConsoleAPP;

using Newtonsoft.Json;
using System;
using System.Text.Json;
using System.Threading.Tasks;

internal class Program
{
    static public string EmailWithoutTailFunc(string email)
    {
        string str = "";

        for(int i = 0; i < email.Length-10; i++)
        {
            str += email[i];
        }

        Console.WriteLine(str);
        return str;
    }
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

        help = help / 3600;

        string correctedTime = help.ToString();
        correctedTime += " h";

        return correctedTime;
    }
    public static async Task Main(string[] args)
    {

        Console.WriteLine("\t\t\t\t\t WELCOME TO JIRA - PMS DATA MIGRATION\n\n");

        Console.WriteLine("-> User Authentication \n");

        Console.WriteLine("Enter your JIRA Cloud UserName");

        string EmailId = Console.ReadLine();

        string EmailWithoutTail = EmailWithoutTailFunc(EmailId);

        Console.WriteLine("Enter your JIRA Cloud Access Token");

        string ApiToken = Console.ReadLine();


        Class1 obj = new Class1(EmailId, ApiToken);

        int BoardId;
        bool BoardIdConfirmation = false;
        bool ProcessIsSprint = false;

        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
        };

        //Below Code Meaning - Basic Console App Interface for User Experience which provide basic information related to boards and project and user needs to select any one board or project from the available list using it's board id and program will return all the issues or task which are associated with that particular project.

        while (true)
        {
            try
            {
                var responseAllBoard = await obj.GetResponse($"https://{EmailWithoutTail}.atlassian.net/rest/agile/1.0/board/", null);
                JiraBoardResponse Board = JsonConvert.DeserializeObject<JiraBoardResponse>(responseAllBoard);

                Console.WriteLine("\n-> List of Boards present in your JIRA Account\n");

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

                Console.WriteLine($"An Error Occured Check Your Credentials \n Error :- {ex.ToString()}");
                return;
            }

            // Below Code Meaning - Now untill now user have inputted the board id and now from here i have first fetched the API which provide me list of issues available inside the board and then by deserializing it to an object I'll do the mapping to PMS JSON Schema Class which helps in Data Migration.

            var responseAllIssues = await obj.GetResponse($"https://{EmailWithoutTail}.atlassian.net/rest/agile/1.0/board/{BoardId}/issue", null);

            JiraTask jiraTask = JsonConvert.DeserializeObject<JiraTask>(responseAllIssues);

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
                        startDate = GiveCorrectDateFormat(issue.Fields.Created) ?? "N/A",
                        endDate = GiveCorrectDateFormat(issue.Fields.DueDate) ?? "N/A",
                        status = issue.Fields.Status?.Name ?? "N/A",
                        priority = issue.Fields.Priority?.Name ?? "N/A",
                        owner = new()
                        {
                            id = issue.Fields.Creator?.AccountId?.ToString() ?? "N/A",
                            name = issue.Fields.Creator?.DisplayName ?? "N/A",
                            email = issue.Fields.Creator?.EmailAddress ?? "N/A",
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
                        id = issue.Id.ToString() ?? "N/A",
                        title = issue.Key ?? "N/A",
                        description = issue.Fields.Description ?? "N/A",
                        type = issue.Fields.IssueType?.Name ?? "N/A",
                        status = issue.Fields.Status?.Name ?? "N/A",
                        assignees = new List<Assignee1>(),
                        reporter = new()
                        {
                            id = issue.Fields.Reporter?.AccountId.ToString() ?? "N/A",
                            name = issue.Fields.Reporter?.DisplayName ?? "N/A",
                            email = issue.Fields.Reporter?.EmailAddress ?? "N/A",
                        },
                        priority = issue.Fields.Priority?.Name ?? "N/A",
                        startDate = GiveCorrectDateFormat(issue.Fields.CustomField_10015),
                        dueDate = GiveCorrectDateFormat(issue.Fields.DueDate),
                        subtasks = new List<SubTask1>(),
                        comments = new List<Comment1>(),
                        attachments = new List<Attachment1>(),
                        resolution = "N/A",
                        timeEstimate = GiveCorrectTimeEstimate(issue.Fields.TimeEstimate) ?? "N/A",
                        tags = issue.Fields.Labels ?? [],
                        timelogs = new()
                        {
                            id = "N/A",
                            user = { },
                            timeSpent = issue.Fields.TimeEstimate ?? "N/A",
                            dateLogged = "N/A"
                        }
                    };

                    // For Sub Task

                    foreach (var subtask in issue.Fields.SubTasks)
                    {

                        if (subtask != null)
                        {
                            SubTask1 stask = new SubTask1()
                            {
                                id = subtask.Id.ToString() ?? "N/A",
                                title = subtask.Key ?? "N/A",
                                status = subtask.Fields.Status?.Name ?? "N/A"
                            };
                            task.subtasks.Add(stask);
                        }
                    }

                    if (issue.Fields.Assignee != null)
                    {
                        Assignee1 assignee = new Assignee1()
                        {
                            id = issue.Fields.Assignee.AccountId ?? "N/A",
                            name = issue.Fields.Assignee.DisplayName ?? "N/A",
                            email = issue.Fields.Assignee.EmailAddress ?? "N/A"
                        };
                        task.assignees.Add(assignee);
                    }


                    foreach (var comment in issue.Fields.Comment?.Comments)
                    {
                        Comment1 comment1 = new Comment1()
                        {
                            id = comment.id.ToString() ?? "N/A",
                            text = comment.Body ?? "N/A",
                            timestamp = comment.Created,
                            author = new()
                            {
                                id = comment.Author.AccountId?.ToString() ?? "N/A",
                                name = comment.Author.DisplayName ?? "N/A",
                                email = comment.Author.EmailAddress ?? "N/A"
                            }
                        };

                        task.comments.Add(comment1);
                    }

                    foreach (var attachment in issue.Fields.Attachment)
                    {
                        Attachment1 attachment1 = new Attachment1()
                        {
                            id = attachment.Id.ToString() ?? "N/A",
                            fileName = attachment.FileName ?? "N/A",
                            fileType = attachment.MimeType ?? "N/A",
                            fileSize = (attachment.Size / 1024),
                            uploadDate = GiveCorrectDateFormat(attachment.Created.ToString()) ?? "N/A",
                            url = $"https://{EmailWithoutTail}.atlassian.net/rest/api/3/attachment/content/{attachment.Id}" ?? "N/A"
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
                Console.WriteLine($"An Error Occured Check Your Credentials \n Error :- {ex.ToString()}");
                return;
            }

            string jsonString = System.Text.Json.JsonSerializer.Serialize(project, options);

            Console.WriteLine(jsonString + "\n");

            Console.WriteLine("Would you like to quit this application ? If Yes then Press Y or N for No");
            char QuitConfirmation = Convert.ToChar(Console.ReadLine());

            if(QuitConfirmation == 'Y' || QuitConfirmation == 'y')
            {
                Console.WriteLine("Thank You for Visting on our Application");
                break;
            }
        }
    }
}