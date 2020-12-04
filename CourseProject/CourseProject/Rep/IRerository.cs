using CourseProject.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CourseProject.Rep
{
    public interface IRepository : IDisposable
    {
        List<SchoolKid> GetSchoolKidList();
        SchoolKid GetSchoolKid(int id);
        void Create(SchoolKid item);
        void Update(SchoolKid item);
        void Delete(int id);
        void Save();
    }

    public class SchoolKidRepository : IRepository
    {
        private SchoolKidContext db;
        public SchoolKidRepository()
        {
            this.db = new SchoolKidContext();
        }
        public List<SchoolKid> GetSchoolKidList()
        {
            return db.SchoolKids.ToList();
        }
        public SchoolKid GetSchoolKid(int id)
        {
            return db.SchoolKids.Find(id);
        }

        public void Create(SchoolKid c)
        {
            db.SchoolKids.Add(c);
        }

        public void Update(SchoolKid c)
        {
            db.Entry(c).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            SchoolKid c = db.SchoolKids.Find(id);
            if (c != null)
                db.SchoolKids.Remove(c);
        }

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}