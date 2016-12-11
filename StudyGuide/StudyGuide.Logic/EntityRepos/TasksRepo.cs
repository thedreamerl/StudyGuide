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
    public class TasksRepo
    {
        public void AddNewTasks(List<string> tasks, DateTime studyPlan, ScheduleViewModel schedule)
        {
            using (var c = new Context())
            {
                List<Tasks> temp = new List<Tasks>();
                foreach (var t in tasks)
                {
                    temp.Add(new Tasks
                    {
                        Name = t,
                        Status = false,
                        StudyPlan = c.StudyPlan.FirstOrDefault(x => x.Begin == studyPlan && x.ScheduleID.SubjectID.Name == schedule.Subject && x.ScheduleID.WorkTypeID.Name == schedule.WorkType)
                    });
                }
                c.Tasks.AddRange(temp.ToArray());
                c.SaveChanges();
            }
        }
        public IEnumerable<TaskViewModel> ShowAll(DateTime studyPlan, ScheduleViewModel schedule)
        {
            using (var c = new Context())
            {
                var result = (from t in c.Tasks
                              where t.StudyPlan.Begin == studyPlan
                              where t.StudyPlan.ScheduleID.SubjectID.Name == schedule.Subject
                              where t.StudyPlan.ScheduleID.WorkTypeID.Name == schedule.WorkType
                              select new TaskViewModel
                              {
                                  Name = t.Name,
                                  Status = t.Status
                              }).ToList();
                return result;
            }
        }
    }
}
