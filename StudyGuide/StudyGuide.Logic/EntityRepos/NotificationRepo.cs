using StudyGuide.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyGuide.Logic.EntityRepos
{
    public static class NotificationRepo
    {

        public static event Action<StudyPlanViewModel, IEnumerable<TaskViewModel>> Notify;
        public static void CheckStudyPlans()
        {
            var studyPlan = Factory.Default.GetStudyPlanRepo().GetStudyPlanToDo();
            if (studyPlan != null)
            {
                foreach (var s in studyPlan)
                {
                    var tasks = Factory.Default.GetTasksRepo().ShowAll(s.Begin, new ScheduleViewModel { Subject = s.Subject, WorkType = s.WorkType });
                    Notify?.Invoke(s,tasks);
                    Factory.Default.GetStudyPlanRepo().DeletePastStudyPlan(s);
                }
            }
        }
    }
}
