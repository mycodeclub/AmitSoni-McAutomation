using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace IARTAutomationApp.Models
{
    public class EmployeePIMap
    {
        public int EmployeePIId { get; set; }
        [DisplayName("Employee Code")]
        [Required(ErrorMessage = "Please Select a Employee Code")]
        public int EmployeeCode { get; set; }
        [DisplayName("Employee E-Mail address")]
         [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Please Input Email Id")]
        public string EmpEmailId { get; set; }
        [DisplayName("Permanent Home Address")]
        [Required(ErrorMessage = "Please Input Permanent Address")]
        public string PermanentAddress { get; set; }
        [DisplayName("Employee Mobile No")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^[0-9]{0,15}$", ErrorMessage = "PhoneNumber should contain only numbers")]
        [Required(ErrorMessage = "Please Input Mobile No")]
        public string MobileNo { get; set; }
        [DataType(DataType.EmailAddress)]
        //[RegularExpression(@"^\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}$", ErrorMessage = "Email is not in proper format")]
        [DisplayName("E-Mail of Next of Kin")]
        [Required(ErrorMessage = "Please Input E-Mail of Next of Kin")]
        public string EmailIdKin { get; set; }
        [DisplayName("Next of Kin Name")]
        [Required(ErrorMessage = "Please Input Name of Next of Kin")]
        public string KinName { get; set; }
        [DisplayName("Address of Next Of Kin")]
        [Required(ErrorMessage = "Please Input Address of Next of Kin")]
        public string AddressNextOfKin { get; set; }
        [DisplayName("State of Next Of Kin")]
        [Required(ErrorMessage = "Please Input State of Next of Kin")]
        public string StateNextOfKin { get; set; }
        [DisplayName("LGA of Next of Kin")]
        [Required(ErrorMessage = "Please Input LGA of Next of Kin")]
        public string LGAextOfKin { get; set; }
        [DisplayName("Relationship")]
        [Required(ErrorMessage = "Please Input Relationship")]
        public string Relation { get; set; }
        [DisplayName("Next Of Kin Phone No")]
        [RegularExpression(@"^[0-9]{0,15}$", ErrorMessage = "PhoneNumber should contain only numbers")]
        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "Please Input Phone No. of Next of Kin")]
        public string PhoneNoNextOfKin { get; set; }
        [DisplayName("Staff Beneficiary Name")]
        [Required(ErrorMessage = "Please Input Staff Beneficiary Name")]
        public string NameOfStaffBenificiary { get; set; }
        [DisplayName("Staff Beneficiary Phone")]
        [RegularExpression(@"^[0-9]{0,15}$", ErrorMessage = "PhoneNumber should contain only numbers")]

        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "Please Input Staff Beneficiary Phone")]

        public string PhoneOfStaffBenificiary { get; set; }
        [DisplayName("Staff Beneficiary Address")]
        [Required(ErrorMessage = "Please Input Staff Beneficiary Address")]
        public string AddressOfStaffBenificiary { get; set; }
        [DisplayName("Employee Status")]
        [Required(ErrorMessage = "Please Input Employee Status")]
        public string EmployeeStatus { get; set; }
        [HiddenInput]
        public System.DateTime CreatedDate { get; set; }
        [HiddenInput]
        public bool IsDeleted { get; set; }
    }
    [MetadataTypeAttribute(typeof(EmployeePIMap))]
    partial class EmployeePI
    {

    }
}