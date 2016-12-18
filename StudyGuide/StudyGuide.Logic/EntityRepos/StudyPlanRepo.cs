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
        public void AddNew(StudyPlanViewModel s)
        {
            using (var c = new Context())
            {
                var temp = s.Begin.AddMinutes(30);
                var temp2 = s.Begin.AddMinutes(-30);
                if (c.StudyPlan.FirstOrDefault(x => x.Begin < temp && x.Begin > temp2) != null)
                    throw new ArgumentException("You have already planned to study at this time another subject");
                c.StudyPlan.Add(new StudyPlan
                {
                    ScheduleID = c.Schedule.First(x => x.SubjectID.Name == s.Subject && x.WorkTypeID.Name == s.WorkType),
                    Begin = s.Begin
                });
                c.SaveChanges();
            }
            AddEvent?.Invoke();
        }

        public IEnumerable<StudyPlanViewModel> ShowAll(ScheduleViewModel schedule)
        {
            using (var c = new Context())
            {
                var result = (from dt in c.StudyPlan
                              where dt.ScheduleID.SubjectID.Name == schedule.Subject
                              where dt.ScheduleID.WorkTypeID.Name == schedule.WorkType
                              select new StudyPlanViewModel
                              {
                                  Begin = dt.Begin,
                                  Subject = dt.ScheduleID.SubjectID.Name,
                                  WorkType = dt.ScheduleID.WorkTypeID.Name
                              }).ToList();
                return result;
            }
        }

        public IEnumerable<StudyPlanViewModel> GetTodayStudyPlans()
        {
            using (var c = new Context())
            {
                var temp = DateTime.Now.Date;
                var temp2 = temp.AddDays(1);
                var result = (from s in c.StudyPlan
                              where s.Begin >= temp && s.Begin < temp2
                              orderby s.Begin
                              select new StudyPlanViewModel
                              {
                                  Begin = s.Begin,
                                  Subject = s.ScheduleID.SubjectID.Name,
                                  WorkType = s.ScheduleID.WorkTypeID.Name
                              }).ToList();
                return result;
            }
        }

        public IEnumerable<StudyPlanViewModel> GetStudyPlanToDo()
        {
            using (var c = new Context())
            {
                var result = (from s in c.StudyPlan
                              where s.Begin <= DateTime.Now
                              select new StudyPlanViewModel
                              {
                                  Begin = s.Begin,
                                  Subject = s.ScheduleID.SubjectID.Name,
                                  WorkType = s.ScheduleID.WorkTypeID.Name
                              }).ToList();
                AddEvent?.Invoke();
                return result;
            }
        }
        public void DeletePastStudyPlan(StudyPlanViewModel s)
        {
            using (var c = new Context())
            {
                var tasksToRemove = from t in c.Tasks
                                    where t.StudyPlan.Begin == s.Begin
                                    select t;
                c.Tasks.RemoveRange(tasksToRemove);
                var studyPlan = (from st in c.StudyPlan
                                 where st.Begin == s.Begin
                                 select st).First();
                c.StudyPlan.Remove(studyPlan);
                c.SaveChanges();
            }
        }
    }
}
