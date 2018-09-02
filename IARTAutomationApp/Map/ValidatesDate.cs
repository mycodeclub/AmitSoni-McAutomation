using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IARTAutomationApp.Map
{
    public class ValidatesDate
    {
        [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
        public sealed class ValidRetireDate : ValidationAttribute
        {
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                if (value != null)
                {
                    DateTime endDate = Convert.ToDateTime(value);
                    if (endDate > DateTime.Now)
                    {
                        return new ValidationResult("End Date can't be greater than Start date");
                    }
                }
                return ValidationResult.Success;
            }
        }
    }
}