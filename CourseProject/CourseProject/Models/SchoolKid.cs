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
        public string Login { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
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