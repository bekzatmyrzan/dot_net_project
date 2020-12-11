﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CourseProject.Models
{
    public class Subject
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Write Name")]
        public string Name { get; set; }

        public int? TeacherId { get; set; }
        public Teacher Teacher { get; set; }

        public int? GroupId { get; set; }
        public Group Group { get; set; }
    }
}