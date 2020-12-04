using CourseProject.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace CourseProject.Controllers
{
    public class HomeController : Controller
    {
        SchoolKidContext db = new SchoolKidContext();
        public bool isAuthenticate()
        {
            if (Session["currentUser"] != null)
            {
                if (Session["role"] != null)
                {
                    return true;
                }
            }
            return false;
        }
        public bool IsAdmin()
        {
            if (Session["role"] != null)
            {
                string role = (string)Session["role"];
                if (role == "Admin")
                {
                    return true;
                }
            }
            return false;
        }
        public bool IsParent()
        {
            if (Session["role"] != null)
            {
                string role = (string)Session["role"];
                if (role == "Parent")
                {
                    return true;
                }
            }
            return false;
        }
        public bool IsTeacher()
        {
            if (Session["role"] != null)
            {
                string role = (string)Session["role"];
                if (role == "Teacher")
                {
                    return true;
                }
            }
            return false;
        }
        public bool IsSchoolKid()
        {
            if (Session["role"] != null)
            {
                string role = (string)Session["role"];
                if (role == "SchoolKid")
                {
                    return true;
                }
            }
            return false;
        }
        public ActionResult Index()
        {
            if (!isAuthenticate())
            {
                return Redirect("/Admin/LoginPage/?error");
            }
            View(db.Schools);
            return View("Index");
        }

        public ActionResult ShowGrades(int? subjectId, int? schoolKidId)
        {
            if (!isAuthenticate())
            {
                return Redirect("/Admin/LoginPage/?error");
            }
            IQueryable<SchoolKid> schoolKids = db.SchoolKids.Include(s => s.Group);
            IQueryable<Subject> subjects = db.Subjects.Include(s => s.Group);

            Subject subject = subjects.First(s => s.Id == subjectId);
            SchoolKid schoolKid = schoolKids.First(s => s.Id == schoolKidId);


            IQueryable<Grade> grades = db.Grades.Include(g => g.SchoolKid).Include(g => g.Subject);
            grades = grades.Where(g => g.SchoolKidId == schoolKidId);
            grades = grades.Where(g => g.SubjectId == subjectId);

            ViewBag.subject = subject;
            ViewBag.schoolKid = schoolKid;

            return View(grades);

        }

        public ActionResult PutGrade(int? subjectId, int? schoolKidId)
        {
            if (!isAuthenticate())
            {
                return Redirect("/Admin/LoginPage/?error");
            }
            if (!IsAdmin())
            {
                if (!IsTeacher())
                {
                    return Redirect("/Admin/LoginPage/?noPermission");
                }
            }
            ViewBag.SchoolKidId = new SelectList(db.SchoolKids, "Id", "Name");
            ViewBag.SubjectId = new SelectList(db.Subjects, "Id", "Name");

            IQueryable<SchoolKid> schoolKids = db.SchoolKids.Include(s => s.Group);
            IQueryable<Subject> subjects = db.Subjects.Include(s => s.Group);

            Subject subject = subjects.First(s => s.Id == subjectId);
            SchoolKid schoolKid = schoolKids.First(s => s.Id == schoolKidId);

            ViewBag.subject = subject;
            ViewBag.schoolKid = schoolKid;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> PutGrade([Bind(Include = "Id,Value,SchoolKidId,SubjectId,Date")] Grade grade)
        {
            if (!isAuthenticate())
            {
                return Redirect("/Admin/LoginPage/?error");
            }
            if (!IsAdmin())
            {
                if (!IsTeacher())
                {
                    return Redirect("/Admin/LoginPage/?noPermission");
                }
            }
            if (ModelState.IsValid)
            {
                db.Grades.Add(grade);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.SchoolKidId = new SelectList(db.SchoolKids, "Id", "Name", grade.SchoolKidId);
            ViewBag.SubjectId = new SelectList(db.Subjects, "Id", "Name", grade.SubjectId);
            return View(grade);
        }
        
        public async Task<ActionResult> SubjectDetails(int? subjectId)
        {
            if (!isAuthenticate())
            {
                return Redirect("/Admin/LoginPage/?error");
            }
            if (subjectId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IQueryable<SchoolKid> schoolKids = db.SchoolKids.Include(s => s.Group);
            IQueryable<Subject> subjects = db.Subjects.Include(s => s.Group);

            Subject subject = subjects.First(s=>s.Id == subjectId);
            schoolKids = schoolKids.Where(s=>s.GroupId == subject.GroupId);

            if (schoolKids == null)
            {
                return HttpNotFound();
            }
            ViewBag.subjectId = subject.Id;
            return View(schoolKids);
        }

        public ActionResult TeacherPage(int? teacher)
        {
            if (!isAuthenticate())
            {
                return Redirect("/Admin/LoginPage/?error");
            }
            if (!IsAdmin())
            {
                if (!IsTeacher())
                {
                    return Redirect("/Admin/LoginPage/?noPermission");
                }
            }
            IQueryable<Subject> subjects = db.Subjects.Include(s => s.Teacher).Include(s => s.Group);
            if (teacher != null && teacher != 0)
            {
                if (IsAdmin())
                {
                    subjects = subjects.Where(s => s.TeacherId == teacher);
                }
                else {
                    return Redirect("/Admin/LoginPage/?noPermission");
                }
            }
            else
            {
                IQueryable<Teacher> teachers0 = db.Teachers;
                Teacher teacher1 = null;
                string email = (string)Session["currentUser"];
                foreach (Teacher t in teachers0)
                {
                    if (t.Login == email)
                    {
                        teacher1 = t;
                        break;
                    }
                }
                if (teacher1 != null)
                {
                    subjects = subjects.Where(s => s.TeacherId == teacher1.Id);
                }
            }

            List<Teacher> teachers = db.Teachers.ToList();
            // устанавливаем начальный элемент, который позволит выбрать всех
            //teachers.Insert(0, new Teacher { Name = "Все", Id = 0 });

            SubjectListViewModel plvm = new SubjectListViewModel
            {
                Subjects = subjects.Include(s => s.Teacher).Include(s => s.Group).ToList(),
                Teachers = new SelectList(teachers, "Id", "Name")
            };
            return View(plvm);
        }

        public ActionResult SchoolKidPage(int? schoolKid)
        {
            if (!isAuthenticate())
            {
                return Redirect("/Admin/LoginPage/?error");
            }
            IQueryable<Grade> grades = db.Grades.Include(s => s.SchoolKid).Include(s => s.Subject);
            if (schoolKid != null && schoolKid != 0)
            {
                if (!IsSchoolKid())
                {
                    grades = grades.Where(s => s.SchoolKidId == schoolKid);
                }
                else {
                    return Redirect("/Admin/LoginPage/?noPermission");
                }
            }
            else
            {
                IQueryable<SchoolKid> schoolKids0 = db.SchoolKids;
                SchoolKid schoolKid1 = null;
                string email = (string)Session["currentUser"];
                foreach (SchoolKid s in schoolKids0)
                {
                    if (s.Login == email)
                    {
                        schoolKid1 = s;
                        break;
                    }
                }
                if (schoolKid != null)
                {
                    grades = grades.Where(s => s.SchoolKidId == schoolKid1.Id);
                }
            }

            List<SchoolKid> schoolKids = db.SchoolKids.ToList();
            // устанавливаем начальный элемент, который позволит выбрать всех
            schoolKids.Insert(0, new SchoolKid { Name = "Все", Id = 0 });

            GradeListViewModel plvm = new GradeListViewModel
            {
                Grades = grades.ToList(),
                SchoolKids = new SelectList(schoolKids, "Id", "Name")
            };
            return View(plvm);
        }

        public ActionResult ParentPage(int? parent)
        {
            if (!isAuthenticate())
            {
                return Redirect("/Admin/LoginPage/?error");
            }
            if (!IsAdmin())
            {
                if (!IsParent())
                {
                    return Redirect("/Admin/LoginPage/?noPermission");
                }
            }
            IQueryable<Grade> grades = db.Grades.Include(s => s.SchoolKid).Include(s => s.Subject);
            IQueryable<SchoolKid> schoolKids1 = db.SchoolKids.Include(s => s.Mother).Include(s => s.Father);
            if (parent != null && parent != 0)
            {
                if (IsAdmin())
                {
                    SchoolKid schoolKid = schoolKids1.First(s => s.MotherId == parent);
                    if (schoolKid == null)
                    {
                        schoolKid = schoolKids1.First(s => s.FatherId == parent);
                    }
                    grades = grades.Where(s => s.SchoolKidId == schoolKid.Id);
                }
                else
                {
                    return Redirect("/Admin/LoginPage/?noPermission");
                }
            }
            else {
                IQueryable<Parent> parents1 = db.Parents;
                Parent parent1 = null;
                string email = (string)Session["currentUser"];
                foreach (Parent p in parents1)
                {
                    if (p.Login == email)
                    {
                        parent1 = p;
                        break;
                    }
                }
                if (parent1 != null)
                {
                    SchoolKid schoolKid = schoolKids1.First(s => s.MotherId == parent1.Id);
                    if (schoolKid == null)
                    {
                        schoolKid = schoolKids1.First(s => s.FatherId == parent1.Id);
                    }
                    grades = grades.Where(s => s.SchoolKidId == schoolKid.Id);
                }
            }

            List<Parent> parents = db.Parents.ToList();
            // устанавливаем начальный элемент, который позволит выбрать всех
            parents.Insert(0, new Parent { Name = "Все", Id = 0 });

            GradeListViewModel plvm = new GradeListViewModel
            {
                Grades = grades.Include(s => s.SchoolKid).Include(s => s.Subject).ToList(),
                Parents = new SelectList(parents, "Id", "Name")
            };
            return View(plvm);
        }
        //
        /*

        [HttpPost]
        public ActionResult DeleteSchoolKid(int id)
        {
            SchoolKid s = db.SchoolKids.Find(id);
            if (s == null)
            {
                return HttpNotFound();
            }
            db.SchoolKids.Remove(s);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost, ActionName("DeleteSchoolKid")]
        public void DeleteSchoolKidConfirmed(int id)
        {
            SchoolKid s = db.SchoolKids.Find(id);
            db.SchoolKids.Remove(s);
            db.SaveChanges();
        }

        [HttpGet]
        public ActionResult CreateSchoolKid()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateSchoolKid(SchoolKid schoolKid)
        {
            db.SchoolKids.Add(schoolKid);
            db.SaveChanges();

            return RedirectToAction("Index");
        }



        [HttpGet]
        public ActionResult KidDetails(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            SchoolKid schoolKid = db.SchoolKids.Find(id);
            if (schoolKid != null)
            {
                return View(schoolKid);//todo
            }
            return HttpNotFound();
        }

        [HttpPost]
        public ActionResult KidDetails(SchoolKid schoolKid)
        {
            db.Entry(schoolKid).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult PutGrade(int id)
        {
            if (id > 10) {
                return Redirect("/Home/Index");
            }
            ViewBag.kidId = id;
            return View();
        }

        [HttpPost]
        public string PutGrade(int kidId, string SubjectName, int grade)
        {
            // добавляем информацию о покупке в базу данных
            Grade gr = new Grade();
            string name = "FFF";
            IEnumerable<SchoolKid> schoolKids = db.SchoolKids;
            foreach (SchoolKid kid in schoolKids) {
                if (kid.Id.Equals(kidId)) {
                    name = kid.Name;
                }
            }
            gr.KidName = name;
            gr.grade = grade;
            gr.SubjectName = SubjectName;
            db.Grades.Add(gr);
            // сохраняем в бд все изменения
            db.SaveChanges();
            return "Name:," + gr.KidName + ", Subject:" + gr.SubjectName + ", Grade: " + gr.grade;
        }
        */

    }
}