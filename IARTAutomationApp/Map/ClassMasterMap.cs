using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IARTAutomationApp.Models
{
    public class ClassMasterMap
    {
        [HiddenInput]
        public int RecordId { get; set; }

        [DisplayName("Class Name")]
        [Required(ErrorMessage = "Please Input Class Name")]
        public string ClassName { get; set; }

        [DisplayName("Class Number")]
        [Required(ErrorMessage = "Please Input Class Number")]
        public int ClassNumber { get; set; }

        [DisplayName("Class Status")]
        [Required(ErrorMessage = "Please Select Class Status")]
        public Nullable<int> ClassStatus { get; set; }

        [HiddenInput]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime CreatedDate { get; set; }

        [HiddenInput]
        public bool IsDeleted { get; set; }
        

    }
    [MetadataTypeAttribute(typeof(ClassMasterMap))]
    partial class ClassMaster
    {

    }
}