namespace StudyGuide.DataBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial4 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.StudyPlans", "End");
        }
        
        public override void Down()
        {
            AddColumn("dbo.StudyPlans", "End", c => c.DateTime(nullable: false));
        }
    }
}
