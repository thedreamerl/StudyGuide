using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyGuide.DataBase
{
   public class Context: DbContext
    {
        public DbSet<FlashCards> Flashards{ get; set; }
        public DbSet<Schedule> Schedule { get; set; }
        public DbSet<StudyPlan> StudyPlan { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Tasks> Tasks{ get; set; }
        public DbSet<WorkType> WorkType{ get; set; }

        public Context() : base("localsql")
        {
        }
    }
}
