using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyGuide.DataBase
{
    class StudyPlan
    {
        public int ID { get; set; }
        public Schedule ScheduleID { get; set; }
        public DateTime Begin { get; set; }
        public DateTime End { get; set; }
        public List<Tasks> Tasks { get; set; }
    }
}
