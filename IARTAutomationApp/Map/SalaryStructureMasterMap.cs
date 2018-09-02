 

namespace IARTAutomationApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;
    public partial class SalaryStructureMasterMap
    {
        [HiddenInput]
        public int SalaryScaleId { get; set; }
        [DisplayName("Salary Scale")]
        public string SalaryScale { get; set; }
        [DisplayName("Grade Level")]
        public Nullable<int> GradeLevel { get; set; }
        [DisplayName("Grade Step")]
        public Nullable<int> Step { get; set; }
        [DisplayName("Salary Amount")]
        public Nullable<decimal> SalaryAmount { get; set; }
        [HiddenInput]
        public Nullable<bool> IsActive { get; set; }
        [HiddenInput]
        public Nullable<bool> IsDeleted { get; set; }
       
        [DisplayName("Scale Year")]
        public string ScaleYear { get; set; }
        [HiddenInput]
        public Nullable<System.DateTime> CreatedDate { get; set; }
    }
    [MetadataTypeAttribute(typeof(SalaryStructureMasterMap))]
    partial class SalaryStructureMaster
    {

    }
}
