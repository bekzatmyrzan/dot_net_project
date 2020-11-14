using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourseProject.Models
{
    public class Grade
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public int? SchoolKidId { get; set; }
        public SchoolKid SchoolKid { get; set; }
        public int? SubjectId { get; set; }
        public Subject Subject { get; set; }

        public DateTime Date { get; set; }
    }
}