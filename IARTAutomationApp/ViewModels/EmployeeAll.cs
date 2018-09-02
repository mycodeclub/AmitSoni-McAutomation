using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IARTAutomationApp.Models;
namespace IARTAutomationApp.ViewModels
{
    public class EmployeeAll
    {
        public List<EmployeeGI> employeegi { get; set; }
        public List<EmployeeMI>employeemi { get; set; }
        public List<EmployeeSI> employeesi { get; set; }
        public List<EmployeePI> employeepi { get; set; }
        public List<EmployeeAI> employeeai { get; set; }
        public List<EmpAIAssociation> employeeassociation { get; set; }
        public List<EmpAIConference> employeeconference { get; set; }

        public List<LeaveLedger> employeeleaveledger { get; set; }
    }
}