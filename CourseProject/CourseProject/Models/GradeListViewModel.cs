using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CourseProject.Models
{
    public class GradeListViewModel
    {
        public IEnumerable<Grade> Grades { get; set; }
        public SelectList Parents { get; set; }
        public SelectList SchoolKids { get; set; }
    }
}