using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIRA_PMS_ConsoleAPP
{
    public class Project
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public string Description { get; set; }
        public string ProjectTypeKey { get; set; }
    }
}
