 

namespace IARTAutomationApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class LeaveApplicationMap
    {
        public int LeaveAccId { get; set; }
        public Nullable<int> EmployeeCode { get; set; }
        public string LeaveTypeName { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public Nullable<System.DateTime> LeaveFromDate { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public Nullable<System.DateTime> LeaveToDate { get; set; }
        public Nullable<decimal> NoOfDays { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public Nullable<System.DateTime> AppDate { get; set; }
        public string Status { get; set; }
        public Nullable<bool> IsApproved { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
    }
    [MetadataType(typeof(LeaveApplicationMap))]
    partial class LeaveApplication
    {

    }
}
