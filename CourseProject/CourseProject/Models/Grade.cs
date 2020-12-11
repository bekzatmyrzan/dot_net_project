using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CourseProject.Models
{
    public class Grade
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Put Value")]
        [Range(typeof(int), "0", "10")]
        public int Value { get; set; }
        public int? SchoolKidId { get; set; }
        public SchoolKid SchoolKid { get; set; }
        public int? SubjectId { get; set; }
        public Subject Subject { get; set; }

        [Required(ErrorMessage = "Choose Date")]
        public DateTime Date { get; set; }
    }
}