using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudyGuide.Logic.EntityRepos;

namespace StudyGuide.Logic
{
    public class Factory
    {
        private Factory() { }
        static Factory _default;
        public static Factory Default
        {
            get
            {
                if (_default == null)
                    _default = new Factory();
                return _default;
            }
        }
        ScheduleRepo _scheduleRepo = new ScheduleRepo();
        StudyPlanRepo _studyPlanRepo = new StudyPlanRepo();
        SubjectRepo _subjectRepo = new SubjectRepo();
        WorkTypeRepo _workTypeRepo = new WorkTypeRepo();
        TasksRepo _tasksRepo = new TasksRepo();

        public ScheduleRepo GetScheduleRepo()
        {
            return _scheduleRepo;
        }
        public StudyPlanRepo GetStudyPlanRepo()
        {
            return _studyPlanRepo;
        }
        public SubjectRepo GetSubjectRepo()
        {
            return _subjectRepo;
        }
        public WorkTypeRepo GetWorkTypeRepo()
        {
            return _workTypeRepo;
        }
        public TasksRepo GetTasksRepo()
        {
            return _tasksRepo;
        }
    }
}
