using CourseProject.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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