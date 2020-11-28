using CourseProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CourseProject.Controllers
{
    public class AdminController : Controller
    {
        SchoolKidContext db = new SchoolKidContext();

        // GET: Admin
        public ActionResult Index()
        {
            return View(db.Schools);
        }

        public ActionResult LoginPage()
        {

            return View();
        }
    }
}