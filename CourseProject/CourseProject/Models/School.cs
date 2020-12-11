using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CourseProject.Models
{
    public class School:IValidatableObject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        public ICollection<Group> Groups { get; set; }
        public School()
        {
            Groups = new List<Group>();
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();

            if (string.IsNullOrWhiteSpace(this.Name))
            {
                errors.Add(new ValidationResult("Введите название школы"));
            }
            if (string.IsNullOrWhiteSpace(this.Address))
            {
                errors.Add(new ValidationResult("Введите адрес школы"));
            }
            if (this.Address.Length<3)
            {
                errors.Add(new ValidationResult("Неполный адрес школы"));
            }

            return errors;
        }
    }
}