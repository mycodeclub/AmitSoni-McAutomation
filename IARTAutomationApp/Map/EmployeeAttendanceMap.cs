using IARTAutomationApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IARTAutomationApp.Map
{
    public  class EmployeeAttendanceMap
    {
        public int EmployeeCode { get; set; }
        public System.DateTime OnDate { get; set; }
        public System.DateTime LoginTime { get; set; }
        public System.DateTime LogoutTime { get; set; }
        public string Status { get; set; }
        //Navigation Property
        public virtual ICollection<EmployeeGI> Emp { get; set; }
    }
    [MetadataType(typeof(EmployeeAttendanceMap))]
    partial class EmployeeAttendance
    {

    }
}