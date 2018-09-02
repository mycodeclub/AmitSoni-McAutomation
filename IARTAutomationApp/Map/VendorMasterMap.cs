using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IARTAutomationApp.Models
{
    public class VendorMasterMap
    {
        [HiddenInput]
        public int RecordId { get; set; }

        [DisplayName("Vendor Full Name")]
        [Required(ErrorMessage = "Please Input Vendor Full Name")]
        public string VendorName { get; set; }

        [DisplayName("Vendor Organization Name")]
        [Required(ErrorMessage = "Please Input Vendor Organization Name")]
        public string VendorOrg { get; set; }

        [DisplayName("Vendor Contact No.")]
        [Required(ErrorMessage = "Please Input Vendor Contact No.")]
        public string VendorMob { get; set; }

        [DisplayName("Vendor Alternate Contact No.")]
        [Required(ErrorMessage = "Please Input Vendor Alternate Contact No.")]
        public string VendorAMob { get; set; }

        [DisplayName("Vendor Email Address")]
        [Required(ErrorMessage = "Please Input Vendor Email Address")]
        public string VendorEmail { get; set; }

        [DisplayName("Vendor Representative Details")]
        [Required(ErrorMessage = "Please Input Vendor Representative Details")]
        public string VendorRepDesc { get; set; }

        [DisplayName("Vendor Address Line 1")]
        [Required(ErrorMessage = "Please Input Vendor Address Line 1")]
        public string VendorAdd1 { get; set; }

        [DisplayName("Vendor Address Line 2")]
        [Required(ErrorMessage = "Please Input Vendor Address Line 2")]
        public string VendorAdd2 { get; set; }

        [DisplayName("Vendor City")]
        [Required(ErrorMessage = "Please Input Vendor City")]
        public Nullable<int> VendorCity { get; set; }

        [DisplayName("Vendor State")]
        [Required(ErrorMessage = "Please Input Vendor State")]
        public Nullable<int> VendorState { get; set; }

        [DisplayName("Vendor Country")]
        [Required(ErrorMessage = "Please Input Vendor Country")]
        public Nullable<int> VendorCountry { get; set; }

        [DisplayName("Vendor Zip Code")]
        [Required(ErrorMessage = "Please Input Vendor Zip Code")]
        public string VendorZipCode { get; set; }

        [DisplayName("Vendor TAX/Registration/VAT No.")]
        [Required(ErrorMessage = "Please Input Vendor TAX/Registration/VAT No.")]
        public string VendorTaxDet { get; set; }

        [DisplayName("Vendor Description")]
        [Required(ErrorMessage = "Please Input Vendor Description")]
        public string VendorDesc { get; set; }

        [DisplayName("Vendor Status")]
        [Required(ErrorMessage = "Please Input Vendor Status")]
        public int VendorStatus { get; set; }

        [HiddenInput]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime CreatedDate { get; set; }

        [HiddenInput]
        public Nullable<System.DateTime> UpdatedDate { get; set; }



    }

    [MetadataTypeAttribute(typeof(VendorMasterMap))]
    partial class VendorMaster
    {

    }
}