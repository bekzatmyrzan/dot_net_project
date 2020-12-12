namespace CourseProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _0913te : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Subjects", "Name", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Subjects", "Name", c => c.String(nullable: false));
        }
    }
}
