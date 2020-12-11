namespace CourseProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1501de : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Schools", "Name", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Schools", "Name", c => c.String(nullable: false, maxLength: 50));
        }
    }
}
