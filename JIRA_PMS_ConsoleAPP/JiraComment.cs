using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIRA_PMS_ConsoleAPP
{
    public class CommentAuthor
    {
        public string? AccountId { get; set; }
        public string? EmailAddress { get; set; }
        public string? DisplayName { get; set; }
    }

    public class CommentsJira
    {
        public int id { get; set; }
        public string? Body { get; set; }
        public DateTime? Created { get; set; }
        public CommentAuthor Author { get; set; }
    }
    public class Comment
    {
        public List<CommentsJira> Comments { get; set; }

    }
}
