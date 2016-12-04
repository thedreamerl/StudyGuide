namespace StudyGuide.DataBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FlashCards",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Term = c.String(),
                        Definition = c.String(),
                        Level = c.Int(nullable: false),
                        RepeatInterval = c.Time(nullable: false, precision: 7),
                        ScheduleID_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Schedules", t => t.ScheduleID_ID)
                .Index(t => t.ScheduleID_ID);
            
            CreateTable(
                "dbo.Schedules",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Deadline = c.DateTime(nullable: false),
                        SubjectID_ID = c.Int(),
                        WorkTypeID_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Subjects", t => t.SubjectID_ID)
                .ForeignKey("dbo.WorkTypes", t => t.WorkTypeID_ID)
                .Index(t => t.SubjectID_ID)
                .Index(t => t.WorkTypeID_ID);
            
            CreateTable(
                "dbo.Subjects",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.WorkTypes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.StudyPlans",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Begin = c.DateTime(nullable: false),
                        End = c.DateTime(nullable: false),
                        ScheduleID_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Schedules", t => t.ScheduleID_ID)
                .Index(t => t.ScheduleID_ID);
            
            CreateTable(
                "dbo.Tasks",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Status = c.Boolean(nullable: false),
                        StudyPlan_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.StudyPlans", t => t.StudyPlan_ID)
                .Index(t => t.StudyPlan_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tasks", "StudyPlan_ID", "dbo.StudyPlans");
            DropForeignKey("dbo.StudyPlans", "ScheduleID_ID", "dbo.Schedules");
            DropForeignKey("dbo.FlashCards", "ScheduleID_ID", "dbo.Schedules");
            DropForeignKey("dbo.Schedules", "WorkTypeID_ID", "dbo.WorkTypes");
            DropForeignKey("dbo.Schedules", "SubjectID_ID", "dbo.Subjects");
            DropIndex("dbo.Tasks", new[] { "StudyPlan_ID" });
            DropIndex("dbo.StudyPlans", new[] { "ScheduleID_ID" });
            DropIndex("dbo.Schedules", new[] { "WorkTypeID_ID" });
            DropIndex("dbo.Schedules", new[] { "SubjectID_ID" });
            DropIndex("dbo.FlashCards", new[] { "ScheduleID_ID" });
            DropTable("dbo.Tasks");
            DropTable("dbo.StudyPlans");
            DropTable("dbo.WorkTypes");
            DropTable("dbo.Subjects");
            DropTable("dbo.Schedules");
            DropTable("dbo.FlashCards");
        }
    }
}
