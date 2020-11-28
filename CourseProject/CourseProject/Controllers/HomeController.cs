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

        public ActionResult Index()
        {
            /*
            IEnumerable<SchoolKid> schoolKids = db.SchoolKids;
            IEnumerable<Grade> grades = db.Grades;
            IEnumerable<Subject> subjects = db.Subjects;
            IEnumerable<Teacher> teachers = db.Teachers;
            IEnumerable<School> schools = db.Schools;
            IEnumerable<Parent> parents = db.Parents;
            IEnumerable<Group> groups = db.Groups;
            ViewBag.SchoolKids = schoolKids;
            ViewBag.Grades = grades;
            ViewBag.Subjects = subjects;
            ViewBag.Teachers = teachers;
            ViewBag.Schools = schools;
            ViewBag.Parents = parents;
            ViewBag.Groups = groups;
            */
            // возвращаем представление
            return View(db.Schools);
        }

        public ActionResult ShowGrades(int? subjectId, int? schoolKidId)
        {
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

        public ActionResult LoginPage()
        {
           
            return View();
        }

        public ActionResult TeacherPage(int? teacher)
        {
            IQueryable<Subject> subjects = db.Subjects.Include(s => s.Teacher).Include(s => s.Group);
            if (teacher != null && teacher != 0)
            {
                subjects = subjects.Where(s => s.TeacherId == teacher);
            }

            List<Teacher> teachers = db.Teachers.ToList();
            // устанавливаем начальный элемент, который позволит выбрать всех
            teachers.Insert(0, new Teacher { Name = "Все", Id = 0 });

            SubjectListViewModel plvm = new SubjectListViewModel
            {
                Subjects = subjects.Include(s => s.Teacher).Include(s => s.Group).ToList(),
                Teachers = new SelectList(teachers, "Id", "Name")
            };
            return View(plvm);
        }

        public ActionResult SchoolKidPage(int? schoolKid)
        {
            IQueryable<Grade> grades = db.Grades.Include(s => s.SchoolKid).Include(s => s.Subject);
            if (schoolKid != null && schoolKid != 0)
            {
                grades = grades.Where(s => s.SchoolKidId == schoolKid);
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
            IQueryable<Grade> grades = db.Grades.Include(s => s.SchoolKid).Include(s => s.Subject);
            IQueryable<SchoolKid> schoolKids1 = db.SchoolKids.Include(s => s.Mother).Include(s => s.Father);
            if (parent != null && parent != 0)
            {
                SchoolKid schoolKid = schoolKids1.First(s => s.MotherId == parent);
                if (schoolKid == null) {
                    schoolKid = schoolKids1.First(s => s.FatherId == parent);
                }
                grades = grades.Where(s => s.SchoolKidId == schoolKid.Id);
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