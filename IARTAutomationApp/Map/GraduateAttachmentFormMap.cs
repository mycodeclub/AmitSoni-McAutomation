 

namespace IARTAutomationApp.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class GraduateAttachmentFormMap
    {
        public int Id { get; set; }
        public Nullable<int> EmployeeCode { get; set; }
        public string Name { get; set; }
        public string LetterDated { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string OfficerInCharge { get; set; }
        public string PrincipalAccountant { get; set; }
        public string ReinstatePayment { get; set; }
        public string PaymentToDate { get; set; }
        public string PaymentFromDate { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; }
    
        public virtual EmployeeGI EmployeeGI { get; set; }
        public virtual EmployeeGI EmployeeGI1 { get; set; }
    }
}
