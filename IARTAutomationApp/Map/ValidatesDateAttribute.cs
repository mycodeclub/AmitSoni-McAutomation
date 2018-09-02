using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace IARTAutomationApp.Models
{
    internal class ValidatesDateAttribute :ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                DateTime endDate = Convert.ToDateTime(value);
            
                if (endDate > DateTime.Now)
                {
                    return new ValidationResult("To Date must be greater than From date");
                }
            }
            return ValidationResult.Success;
        }
    }
    internal class ValidatesDatePromotionAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                DateTime endDate = Convert.ToDateTime(value);

                if (endDate > DateTime.Now)
                {
                    return new ValidationResult("Promotion Date must be less than Current Date");
                }
            }
            return ValidationResult.Success;
        }
    }
}