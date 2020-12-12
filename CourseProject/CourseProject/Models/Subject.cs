using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CourseProject.Models
{
    public class Subject
    {
        public int Id { get; set; }
        [Remote("CheckName", "Subjects", ErrorMessage = "Name is not valid.")]
        public string Name { get; set; }

        public int? TeacherId { get; set; }
        public Teacher Teacher { get; set; }

        public int? GroupId { get; set; }
        public Group Group { get; set; }
    }
}