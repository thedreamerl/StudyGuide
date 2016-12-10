using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyGuide.DataBase.DBO
{
    public class Schedule
    {
        public int ID { get; set; }
        public Subject SubjectID { get; set; }
        public WorkType WorkTypeID { get; set; }
        public DateTime Deadline { get; set; }
    }
}
