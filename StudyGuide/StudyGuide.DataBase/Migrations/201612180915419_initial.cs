namespace StudyGuide.DataBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Tasks", "Status");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tasks", "Status", c => c.Boolean(nullable: false));
        }
    }
}
