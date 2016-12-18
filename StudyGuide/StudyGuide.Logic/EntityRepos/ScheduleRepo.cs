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
                if (c.Schedule.FirstOrDefault(x => x.SubjectID.Name == el.Subject && x.WorkTypeID.Name == el.WorkType) != null)
                    throw new ArgumentException("Deadline for this subject and worktype does already exists");

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
        public void DeleteSchedule(ScheduleViewModel s)
        {
            using (var c = new Context())
            {
                var studyPlans = Factory.Default.GetStudyPlanRepo().ShowAll(s);
                foreach (var p in studyPlans)
                {
                    Factory.Default.GetStudyPlanRepo().DeletePastStudyPlan(p);
                }
                var temp = from fc in c.FlashCards
                           where fc.ScheduleID.SubjectID.Name == s.Subject && fc.ScheduleID.WorkTypeID.Name == s.WorkType
                           select fc;
                c.FlashCards.RemoveRange(temp);
                var temp2 = c.Schedule.First(x => x.SubjectID.Name == s.Subject && x.WorkTypeID.Name == s.WorkType);
                c.Schedule.Remove(temp2);
                c.SaveChanges();
            }
        }
    }
}
