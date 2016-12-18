using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyGuide.DataBase.DBO
{
  public class StudyPlan
    {
        public int ID { get; set; }
        public Schedule ScheduleID { get; set; }
        public DateTime Begin { get; set; }
    }
}
