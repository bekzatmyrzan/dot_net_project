using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CourseProject.Models
{
    public class SchoolKidListViewModel
    {
        public IEnumerable<SchoolKid> SchoolKids { get; set; }
        public SelectList Schools { get; set; }
        public SelectList Groups { get; set; }
    }
}