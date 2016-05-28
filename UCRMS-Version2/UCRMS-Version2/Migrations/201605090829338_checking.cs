namespace UCRMS_Version2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class checking : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Courses", "Student_Id", "dbo.Students");
            DropIndex("dbo.Courses", new[] { "Student_Id" });
            CreateTable(
                "dbo.Classrooms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DepartmentId = c.Int(nullable: false),
                        CourseId = c.Int(nullable: false),
                        ClassroomId = c.Int(nullable: false),
                        Day = c.String(nullable: false),
                        ClassStartFrom = c.String(nullable: false),
                        ClassEndAt = c.String(nullable: false),
                        Room_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: false)
                .ForeignKey("dbo.Departments", t => t.DepartmentId, cascadeDelete: false)
                .ForeignKey("dbo.Rooms", t => t.Room_Id)
                .Index(t => t.DepartmentId)
                .Index(t => t.CourseId)
                .Index(t => t.Room_Id);
            
            CreateTable(
                "dbo.Grades",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StudentCourses",
                c => new
                    {
                        Student_Id = c.Int(nullable: false),
                        Course_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Student_Id, t.Course_Id })
                .ForeignKey("dbo.Students", t => t.Student_Id, cascadeDelete: false)
                .ForeignKey("dbo.Courses", t => t.Course_Id, cascadeDelete: false)
                .Index(t => t.Student_Id)
                .Index(t => t.Course_Id);
            
            DropColumn("dbo.Courses", "Student_Id");
            DropColumn("dbo.Teachers", "RemainingCredit");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Teachers", "RemainingCredit", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Courses", "Student_Id", c => c.Int());
            DropForeignKey("dbo.Classrooms", "Room_Id", "dbo.Rooms");
            DropForeignKey("dbo.StudentCourses", "Course_Id", "dbo.Courses");
            DropForeignKey("dbo.StudentCourses", "Student_Id", "dbo.Students");
            DropForeignKey("dbo.Classrooms", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.Classrooms", "CourseId", "dbo.Courses");
            DropIndex("dbo.StudentCourses", new[] { "Course_Id" });
            DropIndex("dbo.StudentCourses", new[] { "Student_Id" });
            DropIndex("dbo.Classrooms", new[] { "Room_Id" });
            DropIndex("dbo.Classrooms", new[] { "CourseId" });
            DropIndex("dbo.Classrooms", new[] { "DepartmentId" });
            DropTable("dbo.StudentCourses");
            DropTable("dbo.Grades");
            DropTable("dbo.Classrooms");
            CreateIndex("dbo.Courses", "Student_Id");
            AddForeignKey("dbo.Courses", "Student_Id", "dbo.Students", "Id");
        }
    }
}
