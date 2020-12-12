using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CourseProject.Models;

namespace CourseProject.Controllers
{
    public class SubjectsController : Controller
    {
        private SchoolKidContext db = new SchoolKidContext();
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
        // GET: Subjects
        public ActionResult Index()
        {
            if (!isAuthenticate())
            {
                return Redirect("/Admin/LoginPage/?error");
            }
            var subjects = db.Subjects.Include(s => s.Group).Include(s => s.Teacher);
            View(subjects);
            return View("Index");
        }

        // GET: Subjects/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (!isAuthenticate())
            {
                return Redirect("/Admin/LoginPage/?error");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subject subject = await db.Subjects.FindAsync(id);
            if (subject == null)
            {
                return HttpNotFound();
            }
            return View(subject);
        }

        // GET: Subjects/Create
        public ActionResult Create()
        {
            if (!isAuthenticate())
            {
                return Redirect("/Admin/LoginPage/?error");
            }
            ViewBag.GroupId = new SelectList(db.Groups, "Id", "Name");
            ViewBag.TeacherId = new SelectList(db.Teachers, "Id", "Name");
            return View();
        }

        // POST: Subjects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,TeacherId,GroupId")] Subject subject)
        {
            if (!isAuthenticate())
            {
                return Redirect("/Admin/LoginPage/?error");
            }
            if (ModelState.IsValid)
            {
                db.Subjects.Add(subject);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.GroupId = new SelectList(db.Groups, "Id", "Name", subject.GroupId);
            ViewBag.TeacherId = new SelectList(db.Teachers, "Id", "PhoneNumber", subject.TeacherId);
            return View(subject);
        }

        // GET: Subjects/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (!isAuthenticate())
            {
                return Redirect("/Admin/LoginPage/?error");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subject subject = await db.Subjects.FindAsync(id);
            if (subject == null)
            {
                return HttpNotFound();
            }
            ViewBag.GroupId = new SelectList(db.Groups, "Id", "Name", subject.GroupId);
            ViewBag.TeacherId = new SelectList(db.Teachers, "Id", "PhoneNumber", subject.TeacherId);
            return View(subject);
        }

        // POST: Subjects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,TeacherId,GroupId")] Subject subject)
        {
            if (!isAuthenticate())
            {
                return Redirect("/Admin/LoginPage/?error");
            }
            if (ModelState.IsValid)
            {
                db.Entry(subject).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.GroupId = new SelectList(db.Groups, "Id", "Name", subject.GroupId);
            ViewBag.TeacherId = new SelectList(db.Teachers, "Id", "PhoneNumber", subject.TeacherId);
            return View(subject);
        }

        // GET: Subjects/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (!isAuthenticate())
            {
                return Redirect("/Admin/LoginPage/?error");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subject subject = await db.Subjects.FindAsync(id);
            if (subject == null)
            {
                return HttpNotFound();
            }
            return View(subject);
        }

        // POST: Subjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            if (!isAuthenticate())
            {
                return Redirect("/Admin/LoginPage/?error");
            }
            Subject subject = await db.Subjects.FindAsync(id);
            db.Subjects.Remove(subject);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [HttpGet]
        public JsonResult CheckName(string name)
        {
            var result = (name.Length > 3 ^ name == "name");
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
