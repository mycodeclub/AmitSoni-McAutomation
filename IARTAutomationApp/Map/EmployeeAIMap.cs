using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using IARTAutomationApp.Models;


namespace IARTAutomationApp.Models
{
    public class EmployeeAIMap
    {
        public int EmployeeAIId { get; set; }
        [DisplayName("Employee Code")]
        [Required(ErrorMessage = "Please select a Employee Code")]
        public int EmployeeCode { get; set; }
        [Required(ErrorMessage = "Please select a Institution Attended(1)")]

        [DisplayName("Institution Attended(1)")]
        public string InstitutionAttended1 { get; set; }
        [DisplayName("Institution Attended(2)")]
        [Required(ErrorMessage = "Please select a Institution Attended(2)")]
        public string InstitutionAttended2 { get; set; }
        [Required(ErrorMessage = "Please select a Institution Attended(3)")]

        [DisplayName("Institution Attended(3)")]
        public string InstitutionAttended3 { get; set; }
        [Required(ErrorMessage = "Please select a Qualification(1)")]

        [DisplayName("Qualification(1)")]
        public string Qualification1 { get; set; }
        [Required(ErrorMessage = "Please select a Qualification(2)")]

        [DisplayName("Qualification(2)")]
        public string Qualification2 { get; set; }
        [Required(ErrorMessage = "Please select a Qualification(3)")]

        [DisplayName("Qualification(3)")]
        public string Qualification3 { get; set; }
        [Required(ErrorMessage = "Please select a Year Of Graduation(1)")]

        [DisplayName("Year Of Graduation")]

        public string YearOfGraduation1 { get; set; }
        [DisplayName("Year Of Graduation")]
        [Required(ErrorMessage = "Please select a Year Of Graduation(2)")]

        public string YearOfGraduation2 { get; set; }
        [DisplayName("Year Of Graduation")]
        [Required(ErrorMessage = "Please select a Year Of Graduation(3)")]

        public string YearOfGraduation3 { get; set; }
        [DisplayName("Professional Associations Title")]
        //[Required(ErrorMessage = "Please Input Professional Associations Title")]

        public string ProfessionalAssociationsTitle { get; set; }
        [DisplayName("Professional Associations Id Number")]
        //[Required(ErrorMessage = "Please Input Professional Associations Id")]

        public string ProfessionalAssociationsIdNo { get; set; }
        [DisplayName("Professional Associations Date")]
        //[Required(ErrorMessage = "Please Input Professional Associations Date")]

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]

        public Nullable<System.DateTime> ProfessionalAssociationsDate { get; set; }
        [DisplayName("Conference Attended Name")]
        //[Required(ErrorMessage = "Please Input Conference Attended Name")]

        public string ConferenceAttendedName { get; set; }
        [DisplayName("Conference Attended Title")]
        //[Required(ErrorMessage = "Please Input Conference Attended Title")]

        public string ConferenceAttendedTitle { get; set; }
        [DataType(DataType.Date)]
        ////[Required(ErrorMessage = "Please Input Conference Attended Date")]

        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        [DisplayName("Conference Attended Date")]
        public Nullable<System.DateTime> ConferenceAttendedDate { get; set; }
        [HiddenInput]
        public System.DateTime CreatedDate { get; set; }
        [HiddenInput]
        public bool IsDeleted { get; set; }

        //public virtual EmployeeGI EmployeeGI { get; set; }
    }
    [MetadataTypeAttribute(typeof(EmployeeAIMap))]
    partial class EmployeeAI
    {

    }
}