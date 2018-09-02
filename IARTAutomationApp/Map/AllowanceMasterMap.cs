 

namespace IARTAutomationApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Reflection;
    using System.Web.Mvc;

    public partial class AllowanceMasterMap
    {
        [HiddenInput]
        public int AllowanceId { get; set; }
        [DisplayName("Allowance Type")]
        [Required(ErrorMessage = "Please select Allowance Type")]

        public string AllowanceType { get; set; }

        [DisplayName("Value Method")]
        [Required(ErrorMessage = "Please select Value Method")]

        public string ValueMethod { get; set; }
        [DisplayName("Allowance Head")]
        [Required(ErrorMessage = "Please select Allowance Head")]

        public string AllowanceHead { get; set; }
        [DisplayName("Allowance Amount")]
        [Required(ErrorMessage = "Please input Allowance Amount")]

        public Nullable<decimal> AllowanceAmount { get; set; }
        [DisplayName("Is Active")]

        public Nullable<bool> IsActive { get; set; }
        [HiddenInput]
        public Nullable<bool> IsCreated { get; set; }
        [HiddenInput]
        public Nullable<System.DateTime> CreatedDate { get; set; }
    }

    [MetadataTypeAttribute(typeof(AllowanceMasterMap))]
    partial class AllowanceMaster
    {

    }

}
