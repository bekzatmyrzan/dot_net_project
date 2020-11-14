namespace CourseProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Grades",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.Int(nullable: false),
                        SchoolKidId = c.Int(),
                        SubjectId = c.Int(),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SchoolKids", t => t.SchoolKidId)
                .ForeignKey("dbo.Subjects", t => t.SubjectId)
                .Index(t => t.SchoolKidId)
                .Index(t => t.SubjectId);
            
            CreateTable(
                "dbo.SchoolKids",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Login = c.String(),
                        Password = c.String(),
                        Name = c.String(),
                        Surname = c.String(),
                        MotherId = c.Int(),
                        FatherId = c.Int(),
                        SchoolId = c.Int(),
                        GroupId = c.Int(),
                        Parent_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Parents", t => t.Parent_Id)
                .ForeignKey("dbo.Parents", t => t.FatherId)
                .ForeignKey("dbo.Groups", t => t.GroupId)
                .ForeignKey("dbo.Parents", t => t.MotherId)
                .ForeignKey("dbo.Schools", t => t.SchoolId)
                .Index(t => t.MotherId)
                .Index(t => t.FatherId)
                .Index(t => t.SchoolId)
                .Index(t => t.GroupId)
                .Index(t => t.Parent_Id);
            
            CreateTable(
                "dbo.Parents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Login = c.String(),
                        Password = c.String(),
                        Name = c.String(),
                        Surname = c.String(),
                        PhoneNumber = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        SchoolId = c.Int(),
                        AdviserId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Teachers", t => t.AdviserId)
                .ForeignKey("dbo.Schools", t => t.SchoolId)
                .Index(t => t.SchoolId)
                .Index(t => t.AdviserId);
            
            CreateTable(
                "dbo.Teachers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Login = c.String(),
                        Password = c.String(),
                        Name = c.String(),
                        Surname = c.String(),
                        PhoneNumber = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Subjects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        TeacherId = c.Int(),
                        GroupId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Groups", t => t.GroupId)
                .ForeignKey("dbo.Teachers", t => t.TeacherId)
                .Index(t => t.TeacherId)
                .Index(t => t.GroupId);
            
            CreateTable(
                "dbo.Schools",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Login = c.String(),
                        Password = c.String(),
                        Name = c.String(),
                        Surname = c.String(),
                        RoleId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Roles", t => t.RoleId)
                .Index(t => t.RoleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.Grades", "SubjectId", "dbo.Subjects");
            DropForeignKey("dbo.Grades", "SchoolKidId", "dbo.SchoolKids");
            DropForeignKey("dbo.SchoolKids", "SchoolId", "dbo.Schools");
            DropForeignKey("dbo.SchoolKids", "MotherId", "dbo.Parents");
            DropForeignKey("dbo.SchoolKids", "GroupId", "dbo.Groups");
            DropForeignKey("dbo.Groups", "SchoolId", "dbo.Schools");
            DropForeignKey("dbo.Groups", "AdviserId", "dbo.Teachers");
            DropForeignKey("dbo.Subjects", "TeacherId", "dbo.Teachers");
            DropForeignKey("dbo.Subjects", "GroupId", "dbo.Groups");
            DropForeignKey("dbo.SchoolKids", "FatherId", "dbo.Parents");
            DropForeignKey("dbo.SchoolKids", "Parent_Id", "dbo.Parents");
            DropIndex("dbo.Users", new[] { "RoleId" });
            DropIndex("dbo.Subjects", new[] { "GroupId" });
            DropIndex("dbo.Subjects", new[] { "TeacherId" });
            DropIndex("dbo.Groups", new[] { "AdviserId" });
            DropIndex("dbo.Groups", new[] { "SchoolId" });
            DropIndex("dbo.SchoolKids", new[] { "Parent_Id" });
            DropIndex("dbo.SchoolKids", new[] { "GroupId" });
            DropIndex("dbo.SchoolKids", new[] { "SchoolId" });
            DropIndex("dbo.SchoolKids", new[] { "FatherId" });
            DropIndex("dbo.SchoolKids", new[] { "MotherId" });
            DropIndex("dbo.Grades", new[] { "SubjectId" });
            DropIndex("dbo.Grades", new[] { "SchoolKidId" });
            DropTable("dbo.Users");
            DropTable("dbo.Roles");
            DropTable("dbo.Schools");
            DropTable("dbo.Subjects");
            DropTable("dbo.Teachers");
            DropTable("dbo.Groups");
            DropTable("dbo.Parents");
            DropTable("dbo.SchoolKids");
            DropTable("dbo.Grades");
        }
    }
}
