using StudyGuide.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyGuide.Logic.EntityRepos
{
    class NotificationRepo
    {

        public static event Action<StudyPlanViewModel> Notify;
        public static void CheckStudyPlans()
        {
            var studyPlan = Factory.Default.GetStudyPlanRepo().GetStudyPlanToDo();
            if (studyPlan != null)
            {
                foreach (var s in studyPlan)
                {
                    Notify?.Invoke(s);
                }
            }
        }
    }
}
