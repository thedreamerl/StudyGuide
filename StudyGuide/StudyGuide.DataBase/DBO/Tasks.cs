using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyGuide.DataBase.DBO
{
    public class Tasks
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public StudyPlan StudyPlan { get; set; }
    }
}
