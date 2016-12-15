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
    public class ScheduleRepo
    {
       public event Action UpdateList;
        public void AddNew(ScheduleViewModel el)
        {
            using (var c = new Context())
            {
                c.Schedule.Add(new Schedule
                {
                    SubjectID = c.Subjects.First(x => x.Name == el.Subject),
                    WorkTypeID = c.WorkType.First(x => x.Name == el.WorkType),
                    Deadline = el.Deadline
                });
                c.SaveChanges();
          }
            UpdateList?.Invoke();
            
        }

        public IEnumerable<ScheduleViewModel> ShowAll()
        {
            using (var c = new Context())
            {
                var result = (from s in c.Schedule
                              orderby s.Deadline
                             select new ScheduleViewModel
                             {
                                 Subject = s.SubjectID.Name,
                                 WorkType = s.WorkTypeID.Name,
                                 Deadline = s.Deadline
                             }).ToList();
                return result;
            }
        }
    }
}
