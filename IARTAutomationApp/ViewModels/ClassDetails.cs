using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IARTAutomationApp.ViewModels
{
    public class ClassDetails
    {
        [DisplayName("Class Name")]
        public string className { get; set; }

        [DisplayName("Class Number")]
        public int classNumber { get; set; }
        
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime createdDate { get; set; }

        [DisplayName("Class Created By")]
        public string empName { get; set; }

        [DisplayName("Class Status")]
        public string status { get; set; }

        public int? empid { get; set; }

        public int RecordId { get; set; }
    }
}