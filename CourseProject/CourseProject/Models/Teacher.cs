using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CourseProject.Models
{
    public class Teacher
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
        [Required(ErrorMessage = "Write Phone number")]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 15 символов")]
        public string PhoneNumber { get; set; }
        public ICollection<Subject> Subjects { get; set; }
        public Teacher()
        {
            Subjects = new List<Subject>();
        }
    }
}