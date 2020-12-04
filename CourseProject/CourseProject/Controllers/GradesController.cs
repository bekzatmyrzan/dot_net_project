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
    public class GradesController : Controller
    {
        private SchoolKidContext db = new SchoolKidContext();

        // GET: Grades
        public ActionResult Index()
        {
            var grades = db.Grades.Include(g => g.SchoolKid).Include(g => g.Subject);
            View(grades);
            return View("Index");
        }

        // GET: Grades/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Grade grade = await db.Grades.FindAsync(id);
            if (grade == null)
            {
                return HttpNotFound();
            }
            return View(grade);
        }

        // GET: Grades/Create
        public ActionResult Create()
        {
            ViewBag.SchoolKidId = new SelectList(db.SchoolKids, "Id", "Name");
            ViewBag.SubjectId = new SelectList(db.Subjects, "Id", "Name");
            return View();
        }

        // POST: Grades/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Value,SchoolKidId,SubjectId,Date")] Grade grade)
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

        // GET: Grades/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Grade grade = await db.Grades.FindAsync(id);
            if (grade == null)
            {
                return HttpNotFound();
            }
            ViewBag.SchoolKidId = new SelectList(db.SchoolKids, "Id", "Login", grade.SchoolKidId);
            ViewBag.SubjectId = new SelectList(db.Subjects, "Id", "Name", grade.SubjectId);
            return View(grade);
        }

        // POST: Grades/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Value,SchoolKidId,SubjectId,Date")] Grade grade)
        {
            if (ModelState.IsValid)
            {
                db.Entry(grade).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.SchoolKidId = new SelectList(db.SchoolKids, "Id", "Login", grade.SchoolKidId);
            ViewBag.SubjectId = new SelectList(db.Subjects, "Id", "Name", grade.SubjectId);
            return View(grade);
        }

        // GET: Grades/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Grade grade = await db.Grades.FindAsync(id);
            if (grade == null)
            {
                return HttpNotFound();
            }
            return View(grade);
        }

        // POST: Grades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Grade grade = await db.Grades.FindAsync(id);
            db.Grades.Remove(grade);
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
    }
}
