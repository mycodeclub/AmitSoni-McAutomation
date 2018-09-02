using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IARTAutomationApp.Models;
namespace IARTAutomationApp.ViewModels
{
    public class EmployeeDetails
    {   public EmployeeAI employeeAI { get; set; }
        public EmployeeGI employeeGI { get; set; }
        public EmployeeMI employeeMI { get; set; }
        public EmployeePI employeePI { get; set; }
        public EmployeeSI employeeSI { get; set; }

        public EmpAIAssociation empassociation { get; set; }
        public EmpAIConference empconference { get; set; }

        
    }
}