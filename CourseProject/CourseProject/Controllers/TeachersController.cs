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
    public class TeachersController : Controller
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

        // GET: Teachers
        public ActionResult Index()
        {
            if (!isAuthenticate())
            {
                return Redirect("/Admin/LoginPage/?error");
            }
            View(db.Teachers);
            return View("Index");
        }

        // GET: Teachers/Details/5
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
            Teacher teacher = await db.Teachers.FindAsync(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            return View(teacher);
        }

        // GET: Teachers/Create
        public ActionResult Create()
        {
            if (!isAuthenticate())
            {
                return Redirect("/Admin/LoginPage/?error");
            }
            return View();
        }

        // POST: Teachers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Login,Password,Name,Surname,PhoneNumber")] Teacher teacher)
        {
            if (!isAuthenticate())
            {
                return Redirect("/Admin/LoginPage/?error");
            }
            if (ModelState.IsValid)
            {
                db.Teachers.Add(teacher);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(teacher);
        }

        // GET: Teachers/Edit/5
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
            Teacher teacher = await db.Teachers.FindAsync(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            return View(teacher);
        }

        // POST: Teachers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Login,Password,Name,Surname,PhoneNumber")] Teacher teacher)
        {
            if (!isAuthenticate())
            {
                return Redirect("/Admin/LoginPage/?error");
            }
            if (ModelState.IsValid)
            {
                db.Entry(teacher).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(teacher);
        }

        // GET: Teachers/Delete/5
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
            Teacher teacher = await db.Teachers.FindAsync(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            return View(teacher);
        }

        // POST: Teachers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            if (!isAuthenticate())
            {
                return Redirect("/Admin/LoginPage/?error");
            }
            Teacher teacher = await db.Teachers.FindAsync(id);
            db.Teachers.Remove(teacher);
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
