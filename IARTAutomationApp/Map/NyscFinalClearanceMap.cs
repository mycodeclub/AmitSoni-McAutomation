 

namespace IARTAutomationApp.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class NyscFinalClearanceMap
    {
        public int Id { get; set; }
        public Nullable<int> EmployeeCode { get; set; }
        public string OurRef { get; set; }
        public string YourRef { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public string Name { get; set; }
        public string NYSC_Code { get; set; }
        public Nullable<System.DateTime> EffectDate { get; set; }
        public string BankAccountNo { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; }
    
        public virtual EmployeeGI EmployeeGI { get; set; }
        public virtual EmployeeGI EmployeeGI1 { get; set; }
    }
}
