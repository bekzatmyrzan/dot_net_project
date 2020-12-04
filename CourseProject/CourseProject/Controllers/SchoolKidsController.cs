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
using Nest;
using CourseProject.Rep;

namespace CourseProject.Controllers
{
    public class SchoolKidsController : Controller
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

        private IRepository @object;

        public SchoolKidsController(IRepository @object)
        {
            this.@object = @object;
        }

        public SchoolKidsController()
        {
        }

        // GET: SchoolKids
        public ActionResult Index()
        {
            if (!isAuthenticate())
            {
                return Redirect("/Admin/LoginPage/?error");
            }
            var schoolKids = db.SchoolKids.Include(s => s.Father).Include(s => s.Group).Include(s => s.Mother).Include(s => s.School);
            View(schoolKids);
            return View("Index");
        }

        // GET: SchoolKids
        public ActionResult ListView(int? school, int? group)
        {
            if (!isAuthenticate())
            {
                return Redirect("/Admin/LoginPage/?error");
            }
            IQueryable<SchoolKid> schoolKids = db.SchoolKids.Include(s => s.Father).Include(s => s.Group).Include(s => s.Mother).Include(s => s.School);
            if (school != null && school != 0)
            {
                schoolKids = schoolKids.Where(s => s.SchoolId == school);
            }
            if (group != null && group != 0)
            {
                schoolKids = schoolKids.Where(s => s.GroupId == group);
            }

            List<School> schools = db.Schools.ToList();
            List<Group> groups = db.Groups.ToList();
            // устанавливаем начальный элемент, который позволит выбрать всех
            schools.Insert(0, new School { Name = "Все", Id = 0 });
            groups.Insert(0, new Group { Name = "Все", Id = 0 });

            SchoolKidListViewModel plvm = new SchoolKidListViewModel
            {
                SchoolKids = schoolKids.ToList(),
                Schools = new SelectList(schools, "Id", "Name"),
                Groups = new SelectList(groups, "Id", "Name")
            };
            /*if (this.Session["test"] != null)
            {
                ViewBag.testtest = this.Session["test"];
            }*/

            /*
             <label>@{ if (ViewBag.testtest != null)
                { <a>@ViewBag.testtest</a>} }</label>
             */
            return View(plvm);
        }

        // GET: SchoolKids/Details/5
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
            SchoolKid schoolKid = await db.SchoolKids.FindAsync(id);
            if (schoolKid == null)
            {
                return HttpNotFound();
            }
            return View(schoolKid);
        }

        // GET: SchoolKids/Create
        public ActionResult Create()
        {
            if (!isAuthenticate())
            {
                return Redirect("/Admin/LoginPage/?error");
            }
            if (!IsAdmin())
            {
                return Redirect("/Admin/LoginPage/?noPermission");
            }
            ViewBag.FatherId = new SelectList(db.Parents, "Id", "Login");
            ViewBag.GroupId = new SelectList(db.Groups, "Id", "Name");
            ViewBag.MotherId = new SelectList(db.Parents, "Id", "Login");
            ViewBag.SchoolId = new SelectList(db.Schools, "Id", "Name");
            return View();
        }

        // POST: SchoolKids/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Login,Password,Name,Surname,MotherId,FatherId,SchoolId,GroupId")] SchoolKid schoolKid)
        {
            if (!isAuthenticate())
            {
                return Redirect("/Admin/LoginPage/?error");
            }
            if (!IsAdmin())
            {
                return Redirect("/Admin/LoginPage/?noPermission");
            }
            if (ModelState.IsValid)
            {
                db.SchoolKids.Add(schoolKid);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.FatherId = new SelectList(db.Parents, "Id", "Login", schoolKid.FatherId);
            ViewBag.GroupId = new SelectList(db.Groups, "Id", "Name", schoolKid.GroupId);
            ViewBag.MotherId = new SelectList(db.Parents, "Id", "Login", schoolKid.MotherId);
            ViewBag.SchoolId = new SelectList(db.Schools, "Id", "Name", schoolKid.SchoolId);
            return View(schoolKid);
        }

        // GET: SchoolKids/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (!isAuthenticate())
            {
                return Redirect("/Admin/LoginPage/?error");
            }
            if (!IsAdmin())
            {
                return Redirect("/Admin/LoginPage/?noPermission");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SchoolKid schoolKid = await db.SchoolKids.FindAsync(id);
            if (schoolKid == null)
            {
                return HttpNotFound();
            }
            ViewBag.FatherId = new SelectList(db.Parents, "Id", "Login", schoolKid.FatherId);
            ViewBag.GroupId = new SelectList(db.Groups, "Id", "Name", schoolKid.GroupId);
            ViewBag.MotherId = new SelectList(db.Parents, "Id", "Login", schoolKid.MotherId);
            ViewBag.SchoolId = new SelectList(db.Schools, "Id", "Name", schoolKid.SchoolId);
            return View(schoolKid);
        }

        // POST: SchoolKids/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Login,Password,Name,Surname,MotherId,FatherId,SchoolId,GroupId")] SchoolKid schoolKid)
        {
            if (!isAuthenticate())
            {
                return Redirect("/Admin/LoginPage/?error");
            }
            if (!IsAdmin())
            {
                return Redirect("/Admin/LoginPage/?noPermission");
            }
            if (ModelState.IsValid)
            {
                db.Entry(schoolKid).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.FatherId = new SelectList(db.Parents, "Id", "Login", schoolKid.FatherId);
            ViewBag.GroupId = new SelectList(db.Groups, "Id", "Name", schoolKid.GroupId);
            ViewBag.MotherId = new SelectList(db.Parents, "Id", "Login", schoolKid.MotherId);
            ViewBag.SchoolId = new SelectList(db.Schools, "Id", "Name", schoolKid.SchoolId);
            return View(schoolKid);
        }

        // GET: SchoolKids/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (!isAuthenticate())
            {
                return Redirect("/Admin/LoginPage/?error");
            }
            if (!IsAdmin())
            {
                return Redirect("/Admin/LoginPage/?noPermission");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SchoolKid schoolKid = await db.SchoolKids.FindAsync(id);
            if (schoolKid == null)
            {
                return HttpNotFound();
            }
            return View(schoolKid);
        }

        // POST: SchoolKids/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            if (!isAuthenticate())
            {
                return Redirect("/Admin/LoginPage/?error");
            }
            if (!IsAdmin())
            {
                return Redirect("/Admin/LoginPage/?noPermission");
            }
            SchoolKid schoolKid = await db.SchoolKids.FindAsync(id);
            db.SchoolKids.Remove(schoolKid);
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
