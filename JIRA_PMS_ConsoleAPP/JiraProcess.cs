using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIRA_PMS_ConsoleAPP
{

    public class JiraSprintValue {

        public string? Self { get; set; }
        public int? Id { get; set; }
        public string Name { get; set; }    
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string Goal { get; set; }

    }


    internal class JiraProcess
    {
        public int Total { get; set; }
        public List<JiraSprintValue> Values { get; set; }
    }
}
