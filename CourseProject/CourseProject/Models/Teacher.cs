using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourseProject.Models
{
    public class Teacher
    {

        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public ICollection<Subject> Subjects { get; set; }
        public Teacher()
        {
            Subjects = new List<Subject>();
        }
    }
}