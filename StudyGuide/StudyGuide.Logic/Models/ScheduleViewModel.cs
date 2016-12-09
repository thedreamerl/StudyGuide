using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyGuide.Logic.Models
{
    public class ScheduleViewModel
    {
        public string Subject { get; set; }
        public string WorkType { get; set; }
        public DateTime Deadline { get; set; }
    }
}
