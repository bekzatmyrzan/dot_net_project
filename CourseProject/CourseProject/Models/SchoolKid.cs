using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CourseProject.Models
{
    public class SchoolKid
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Write Email")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Некорректный адрес")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Write Password")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 30 символов")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Write Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Write Surname")]
        public string Surname { get; set; }
        public int? MotherId { get; set; }
        public Parent Mother { get; set; }
        public int? FatherId { get; set; }
        public Parent Father { get; set; }
        public int? SchoolId { get; set; }
        public School School { get; set; }
        public int? GroupId { get; set; }
        public Group Group { get; set; }
    }
}