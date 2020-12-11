namespace CourseProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1434te : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SchoolKids", "Login", c => c.String(nullable: false));
            AlterColumn("dbo.SchoolKids", "Password", c => c.String(nullable: false, maxLength: 30));
            AlterColumn("dbo.SchoolKids", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.SchoolKids", "Surname", c => c.String(nullable: false));
            AlterColumn("dbo.Parents", "Login", c => c.String(nullable: false));
            AlterColumn("dbo.Parents", "Password", c => c.String(nullable: false, maxLength: 30));
            AlterColumn("dbo.Parents", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Parents", "Surname", c => c.String(nullable: false));
            AlterColumn("dbo.Parents", "PhoneNumber", c => c.String(nullable: false, maxLength: 15));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Parents", "PhoneNumber", c => c.String());
            AlterColumn("dbo.Parents", "Surname", c => c.String());
            AlterColumn("dbo.Parents", "Name", c => c.String());
            AlterColumn("dbo.Parents", "Password", c => c.String());
            AlterColumn("dbo.Parents", "Login", c => c.String());
            AlterColumn("dbo.SchoolKids", "Surname", c => c.String());
            AlterColumn("dbo.SchoolKids", "Name", c => c.String());
            AlterColumn("dbo.SchoolKids", "Password", c => c.String());
            AlterColumn("dbo.SchoolKids", "Login", c => c.String());
        }
    }
}
