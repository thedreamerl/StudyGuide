using StudyGuide.DataBase;
using StudyGuide.DataBase.DBO;
using StudyGuide.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyGuide.Logic.EntityRepos
{

    public class StudyPlanRepo
    {
        public event Action AddEvent;
        public void AddNew(DateTime el, ScheduleViewModel schedule)
        {
            using (var c = new Context())
            {
                c.StudyPlan.Add(new StudyPlan
                {
                    ScheduleID = c.Schedule.First(x => x.SubjectID.Name == schedule.Subject && x.WorkTypeID.Name == schedule.WorkType),
                    Begin = el
                });
                c.SaveChanges();
            }
            AddEvent.Invoke();
        }

        public IEnumerable<DateTime> ShowAll(ScheduleViewModel schedule)
        {
            using (var c = new Context())
            {
                var result = (from dt in c.StudyPlan
                              where dt.ScheduleID.SubjectID.Name == schedule.Subject
                              where dt.ScheduleID.WorkTypeID.Name == schedule.WorkType
                              select dt.Begin).ToList();
                return result;
            }
        }
    }
}
