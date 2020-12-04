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
    public class ParentsController : Controller
    {
        SchoolKidContext db = new SchoolKidContext();
        public bool isAuthenticate()
        {
            if (this.Session["currentUser"] != null)
            {
                if (this.Session["role"] != null)
                {
                    return true;
                }
            }
            return false;
        }

        // GET: Parents
        public ActionResult Index()
        {
            if (!isAuthenticate())
            {
                return Redirect("/Admin/LoginPage/?error");
            }
            View( db.Parents);
            return View("Index");
        }

        // GET: Parents/Details/5
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
            Parent parent = await db.Parents.FindAsync(id);
            if (parent == null)
            {
                return HttpNotFound();
            }
            return View(parent);
        }

        // GET: Parents/Create
        public ActionResult Create()
        {
            if (!isAuthenticate())
            {
                return Redirect("/Admin/LoginPage/?error");
            }
            return View();
        }

        // POST: Parents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Login,Password,Name,Surname,PhoneNumber")] Parent parent)
        {
            if (!isAuthenticate())
            {
                return Redirect("/Admin/LoginPage/?error");
            }
            if (ModelState.IsValid)
            {
                db.Parents.Add(parent);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(parent);
        }

        // GET: Parents/Edit/5
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
            Parent parent = await db.Parents.FindAsync(id);
            if (parent == null)
            {
                return HttpNotFound();
            }
            return View(parent);
        }

        // POST: Parents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Login,Password,Name,Surname,PhoneNumber")] Parent parent)
        {
            if (!isAuthenticate())
            {
                return Redirect("/Admin/LoginPage/?error");
            }
            if (ModelState.IsValid)
            {
                db.Entry(parent).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(parent);
        }

        // GET: Parents/Delete/5
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
            Parent parent = await db.Parents.FindAsync(id);
            if (parent == null)
            {
                return HttpNotFound();
            }
            return View(parent);
        }

        // POST: Parents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            if (!isAuthenticate())
            {
                return Redirect("/Admin/LoginPage/?error");
            }
            Parent parent = await db.Parents.FindAsync(id);
            db.Parents.Remove(parent);
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
