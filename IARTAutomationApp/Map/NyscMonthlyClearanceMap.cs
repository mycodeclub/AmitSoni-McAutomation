

namespace IARTAutomationApp.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class NyscMonthlyClearanceMap
    {
        public int Id { get; set; }
        public Nullable<int> EmployeeCode { get; set; }
        public string OurRef { get; set; }
        public string YourRef { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public string Name { get; set; }
        public string NYSC_Code { get; set; }
        public string SatisfactoryMonth { get; set; }
        public string AllowanceMonth { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; }
    
        public virtual EmployeeGI EmployeeGI { get; set; }
        public virtual EmployeeGI EmployeeGI1 { get; set; }
    }
}
