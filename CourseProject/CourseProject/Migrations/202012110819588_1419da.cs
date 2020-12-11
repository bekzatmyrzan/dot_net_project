namespace CourseProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1419da : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Teachers", "Login", c => c.String(nullable: false));
            AlterColumn("dbo.Teachers", "Password", c => c.String(nullable: false, maxLength: 30));
            AlterColumn("dbo.Teachers", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Teachers", "Surname", c => c.String(nullable: false));
            AlterColumn("dbo.Teachers", "PhoneNumber", c => c.String(nullable: false, maxLength: 15));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Teachers", "PhoneNumber", c => c.String());
            AlterColumn("dbo.Teachers", "Surname", c => c.String());
            AlterColumn("dbo.Teachers", "Name", c => c.String());
            AlterColumn("dbo.Teachers", "Password", c => c.String());
            AlterColumn("dbo.Teachers", "Login", c => c.String());
        }
    }
}
