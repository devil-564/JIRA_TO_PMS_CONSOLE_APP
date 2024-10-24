using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIRA_PMS_ConsoleAPP
{

    public class StatusCategory 
    {
        public string Key { get; set; }    
    }
    public class Status
    {
        public string? Name { get; set; }
        public StatusCategory StatusCategory { get; set; }
    }
}
