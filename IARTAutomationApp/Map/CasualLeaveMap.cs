 

namespace IARTAutomationApp.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class CasualLeaveMap
    {
        public int Id { get; set; }
        public int EmployeeCode { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
        public string Post { get; set; }
        public Nullable<System.DateTime> FromDate { get; set; }
        public Nullable<System.DateTime> ToDate { get; set; }
        public string Reason { get; set; }
        public string ResponsiblePerson { get; set; }
        public string HodComment { get; set; }
        public string AnyLeaveDays { get; set; }
        public string OfficeInChargeName { get; set; }
        public string ApprovedDays { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; }
    
        public virtual EmployeeGI EmployeeGI { get; set; }
        public virtual EmployeeGI EmployeeGI1 { get; set; }
    }
}
