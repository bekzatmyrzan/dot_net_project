using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CourseProject.Models
{
    public class SchoolKidInitializer:DropCreateDatabaseAlways<SchoolKidContext>
    {
        protected override void Seed(SchoolKidContext db)
        {
            /*db.SchoolKids.Add(new SchoolKid { Name = "Bekzat", Surname = "Myrzan" });
            db.SchoolKids.Add(new SchoolKid { Name = "Nurbol", Surname = "Lakhaibekov" });
            db.SchoolKids.Add(new SchoolKid { Name = "Erd", Surname = "Serikbay" });
            db.SchoolKids.Add(new SchoolKid { Name = "Dias", Surname = "Ibadulla" });

            db.Parents.Add(new Parent { Name = "Shynar", Surname = "Amanbek",PhoneNumber = "87059674697" });
            db.Parents.Add(new Parent { Name = "Ertis", Surname = "Lakhaibekov",PhoneNumber = "87023332244" });
            db.Parents.Add(new Parent { Name = "Aliya", Surname = "Serikbayeva", PhoneNumber = "87039998877" });
            db.Parents.Add(new Parent { Name = "Maqsat", Surname = "Narbekov", PhoneNumber = "87054586334" });
            db.Parents.Add(new Parent { Name = "Ilyas", Surname = "Ibadullayev", PhoneNumber = "87051115500" });

            db.Teachers.Add(new Teacher { Name = "Elmira", Surname = "Askarova", PhoneNumber = "87001112233" });
            db.Teachers.Add(new Teacher { Name = "Nadeshda", Surname = "Kirillenko", PhoneNumber = "87021231212" });*/

            db.Schools.Add(new School { Name = "Daryn",Address = "Tole bi 72" });
            db.Schools.Add(new School { Name = "KTL",Address = "Momyshuly 50" });

            //db.Groups.Add(new Group { Name = "11A",SchoolName = "Daryn" });
            //db.Groups.Add(new Group { Name = "11B",SchoolName = "Daryn" });

            //db.Subjects.Add(new Subject { Name = "Chemistry",GroupName = "11A",TeacherName = "Elmira" });
            //db.Subjects.Add(new Subject { Name = "History",GroupName = "11B",TeacherName = "Nadeshda" });

            /*
            db.Grades.Add(new Grade { grade = 90,KidName = "Bekzat",SubjectName = "Chemistry"});
            db.Grades.Add(new Grade { grade = 95,KidName = "Nurbol",SubjectName = "Chemistry"});
            db.Grades.Add(new Grade { grade = 85,KidName = "Dias",SubjectName = "History" });
            db.Grades.Add(new Grade { grade = 70,KidName = "Erd",SubjectName = "History" });
            */

            base.Seed(db);
        }
    }
}