namespace StudyGuide.DataBase.Migrations
{
    using DBO;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<StudyGuide.DataBase.Context>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(StudyGuide.DataBase.Context context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            context.Subjects.AddOrUpdate(p => p.Name,
                new Subject { Name = "Math" },
                new Subject { Name = "Programming" },
                new Subject { Name = "English" });
            context.SaveChanges();
            context.WorkType.AddOrUpdate(p => p.Name,
                new WorkType { Name = "Essay" },
                new WorkType { Name = "Test" },
                new WorkType { Name = "Team Project" });
            context.SaveChanges();
            context.Schedule.AddOrUpdate(
                new Schedule
                {
                    SubjectID = context.Subjects.First(p => p.Name == "Math"),
                    WorkTypeID = context.WorkType.First(p => p.Name == "Test"),
                    Deadline = new DateTime(2016, 12, 15, 23, 59, 59)},
                new Schedule
                {
                    SubjectID = context.Subjects.First(p => p.Name == "Programming"),
                    WorkTypeID = context.WorkType.First(p => p.Name == "Team Project"),
                    Deadline = new DateTime(2017, 01, 20, 23, 59, 59)
                });
            context.SaveChanges();
        }
    }
}
