namespace CourseProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1611de : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Schools", "Name", c => c.String());
            AlterColumn("dbo.Schools", "Address", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Schools", "Address", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Schools", "Name", c => c.String(nullable: false));
        }
    }
}
