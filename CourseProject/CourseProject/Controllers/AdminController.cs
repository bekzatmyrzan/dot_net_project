using CourseProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CourseProject.Controllers
{
    public class AdminController : Controller
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

        public bool HavePermission()
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

        // GET: Admin
        public ActionResult Index()
        {
            if (!isAuthenticate())
            {
                return Redirect("/Admin/LoginPage/?notAuthenticate");
            }
            if (!HavePermission())
            {
                return Redirect("/Admin/LoginPage/?noPermission");
            }
            return View(db.Schools);
        }

        public ActionResult LoginPage()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LoginPage(string inputEmail, string inputPassword)
        {
            IQueryable<SchoolKid> schoolKids = db.SchoolKids;
            foreach (SchoolKid schoolKid in schoolKids)
            {
                if (schoolKid.Login == inputEmail)
                {
                    if (schoolKid.Password == inputPassword)
                    {
                        Session["currentUser"] = inputEmail;
                        Session["role"] = "SchoolKid";
                        return Redirect("/Home/SchoolKidPage");
                    }
                }
            }
            IQueryable<Parent> parents = db.Parents;
            foreach (Parent parent in parents)
            {
                if (parent.Login == inputEmail)
                {
                    if (parent.Password == inputPassword)
                    {
                        Session["currentUser"] = inputEmail;
                        Session["role"] = "Parent";
                        return Redirect("/Home/ParentPage");
                    }
                }
            }
            IQueryable<Teacher> teachers = db.Teachers;
            foreach (Teacher teacher in teachers)
            {
                if (teacher.Login == inputEmail)
                {
                    if (teacher.Password == inputPassword)
                    {
                        Session["currentUser"] = inputEmail;
                        Session["role"] = "Teacher";
                        return Redirect("/Home/TeacherPage");
                    }
                }
            }
            if (inputEmail == "admin@test.com") {
                if (inputPassword == "pass123") {
                    Session["currentUser"] = inputEmail;
                    Session["role"] = "Admin";
                    return Redirect("/Home/Index");
                }
            }
            return View();
        }
    }
}