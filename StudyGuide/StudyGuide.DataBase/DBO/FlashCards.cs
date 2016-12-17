using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyGuide.DataBase.DBO
{
    public class FlashCards
    {
        public int ID { get; set; }
        public string Term { get; set; }
        public string Definition { get; set; }
        public Schedule ScheduleID { get; set; }
        public int Level { get; set; }
    }
}
