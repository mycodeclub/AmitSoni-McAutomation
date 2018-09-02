 

namespace IARTAutomationApp.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class LeaveRoasterJuniorStaffMap
    {
        public int Id { get; set; }
        public int EmployeeCode { get; set; }
        public Nullable<int> SNo { get; set; }
        public string Name { get; set; }
        public Nullable<int> NoOfDay { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string FileNo { get; set; }
        public string Contiss { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
    
        public virtual EmployeeGI EmployeeGI { get; set; }
    }
}
