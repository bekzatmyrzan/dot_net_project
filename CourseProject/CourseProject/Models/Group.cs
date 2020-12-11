using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using CourseProject.CustomValidation;

namespace CourseProject.Models
{
    public class Group
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Write Name")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 50 символов")]
        [FirstClass(new string[] { "11A", "11B", "11C", "10A" }, ErrorMessage = "Недопустимое название класса")]
        public string Name { get; set; }
        public int? SchoolId { get; set; }
        public School School { get; set; }
        public int? AdviserId { get; set; }
        public Teacher Adviser { get; set; }

        public ICollection<SchoolKid> SchoolKids { get; set; }
        public Group()
        {
            SchoolKids = new List<SchoolKid>();
        }
    }
}