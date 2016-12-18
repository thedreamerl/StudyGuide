using StudyGuide.DataBase;
using StudyGuide.DataBase.DBO;
using StudyGuide.Logic.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyGuide.Logic.EntityRepos
{
    public class ScheduleRepo
    {
        public event Action UpdateList;
        public async Task AddNew(ScheduleViewModel el)
        {
           using (var c = new Context())
            {
                if (await c.Schedule.FirstOrDefaultAsync(x => x.SubjectID.Name == el.Subject && x.WorkTypeID.Name == el.WorkType) != null)
                    throw new ArgumentException("Deadline for this subject and worktype does already exists");

                c.Schedule.Add(new Schedule
                {
                    SubjectID = c.Subjects.First(x => x.Name == el.Subject),
                    WorkTypeID = c.WorkType.First(x => x.Name == el.WorkType),
                    Deadline = el.Deadline
                });
                await c.SaveChangesAsync();
            }
            UpdateList?.Invoke();
        }

        public Task<IEnumerable<ScheduleViewModel>> ShowAll()
        {
            return Task.Run(() =>
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
                    return (IEnumerable<ScheduleViewModel>)result;
                }
            });
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
                UpdateList?.Invoke();
            }
        }
    }
}
