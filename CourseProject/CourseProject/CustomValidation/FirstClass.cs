using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CourseProject.CustomValidation
{
    public class FirstClass: ValidationAttribute
    {
        private static string[] myClasses;

        public FirstClass(string[] Classes)
        {
            myClasses = Classes;
        }

        public override bool IsValid(object value)
        {
            if (value != null)
            {
                string strval = value.ToString();
                for (int i = 0; i < myClasses.Length; i++)
                {
                    if (strval == myClasses[i])
                        return true;
                }
            }
            return false;
        }
    }
}