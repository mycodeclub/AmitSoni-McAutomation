using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IARTAutomationApp.Models
{
    public class UomMasterMap
    {
        [HiddenInput]
        public int RecordId { get; set; }

        [DisplayName("UOM Full Name")]
        [Required(ErrorMessage = "Please Input UOM Name")]
        public string UOMName { get; set; }

        [DisplayName("UOM Short Code")]
        [Required(ErrorMessage = "Please Input UOM Short Code")]
        public string UOMCode { get; set; }

        [DisplayName("UOM Description")]
        [Required(ErrorMessage = "Please Input UOM Description")]
        public string UOMDesc { get; set; }

        [DisplayName("UOM Status")]
        [Required(ErrorMessage = "Please Select UOM Status")]
        public Nullable<int> UOMStatus { get; set; }

        [HiddenInput]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public System.DateTime CreatedDate { get; set; }

        [HiddenInput]
        public bool IsDeleted { get; set; }

    }

    [MetadataTypeAttribute(typeof(UomMasterMap))]
    partial class UomMaster
    {

    }
}