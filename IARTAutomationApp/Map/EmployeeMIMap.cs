using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace IARTAutomationApp.Models
{
    public class EmployeeMIMap
    {
        public int EmployeeMIId { get; set; }
        [DisplayName("Employee Code")]
        [Required(ErrorMessage = "Please Select a Employee Code")]
        public int EmployeeCode { get; set; }
        [Required(ErrorMessage = "Please Input NHIS No.")]
        [DisplayName("NHIS No")]
        public string NhisNo { get; set; }
        [DisplayName("NHIS PROVIDER")]
        [Required(ErrorMessage = "Please Input NHIS Provider")]
        public string NhisProvider { get; set; }
        [DisplayName("Blood Group")]
        [Required(ErrorMessage = "Please Select Blood Group")]
        public string BloodGroup { get; set; }
        [DisplayName("Blood Genotype")]
        [Required(ErrorMessage = "Please Select Blood Genotype")]
        public string BloodGenotype { get; set; }
        [HiddenInput]
        public System.DateTime CreatedDate { get; set; }
        [HiddenInput]
        public bool IsDeleted { get; set; }


    }
    [MetadataTypeAttribute(typeof(EmployeeMIMap))]
    partial class EmployeeMI
    {

    }
}