namespace CourseProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _202012111223 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Schools", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Schools", "Name", c => c.String());
        }
    }
}
