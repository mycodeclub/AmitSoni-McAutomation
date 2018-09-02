using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using IARTAutomationApp.Models;
namespace IARTAutomationApp.Models
{
    public class EmployeeSIMap
    {
        public int EmployeeSIId { get; set; }
        [DisplayName("Employee Code")]
        public int EmployeeCode { get; set; }
        [DisplayName("Current Posting")]
        public string CurrentPosting { get; set; }
        [DisplayName("Bank Type")]
        public string BankType { get; set; }
        [DisplayName("Name Of Banks")]
        public string NameOfBanks { get; set; }
        [DisplayName("Bank Branch")]
        public string BankBranch { get; set; }
        [DisplayName("Account Type")]
        public string AccountType { get; set; }
        [DisplayName("Account Number")]
        public string AccountNumber { get; set; }
        [DisplayName("Account Name")]
        public string AccountName { get; set; }
        [DisplayName("PFA")]
        public string PFA { get; set; }
        [DisplayName("RSA Pin No")]
        public string RSAPinNo { get; set; }
        [DisplayName("Salary Scale")]
        public string SalaryScale { get; set; }
        [HiddenInput]
        public System.DateTime CreatedDate { get; set; }
        [HiddenInput]
        public bool IsDeleted { get; set; }

        //public virtual EmployeeGI EmployeeGI { get; set; }
    }
    [MetadataTypeAttribute(typeof(EmployeeSIMap))]
    partial class EmployeeSI
    {

    }

}