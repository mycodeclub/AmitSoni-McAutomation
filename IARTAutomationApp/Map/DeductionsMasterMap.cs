 

namespace IARTAutomationApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public partial class DeductionsMasterMap
    {
        [HiddenInput]
        public int DeductionId { get; set; }
        [DisplayName("Deduction Type")]
        [Required(ErrorMessage = "Please select Deduction Type")]

        public string DeductionType { get; set; }
        [DisplayName("Value Method")]
        [Required(ErrorMessage = "Please select Value Method")]

        public string ValueMethod { get; set; }
        [DisplayName("Deduction Head")]
        [Required(ErrorMessage = "Please select Deduction Head")]

        public string DeductionHead { get; set; }
        [DisplayName("Deduction Amount")]
        [Required(ErrorMessage = "Please input Deduction Amount")]

        public Nullable<decimal> DeductionAmount { get; set; }
        [DisplayName("Is Active")]
        public Nullable<bool> IsActive { get; set; }
        [HiddenInput]
        public Nullable<bool> IsCreated { get; set; }
        [HiddenInput]
        public Nullable<System.DateTime> CreatedDate { get; set; }
    }
    [MetadataTypeAttribute(typeof(DeductionsMasterMap))]
    partial class DeductionsMaster
    {

    }
}
