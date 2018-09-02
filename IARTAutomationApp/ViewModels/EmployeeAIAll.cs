using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IARTAutomationApp.Models;
namespace IARTAutomationApp.ViewModels
{
    public class EmployeeAIAll
    {
       
        public EmployeeAI employeeai { get; set; }
        public List<EmpAIAssociation> empassociation { get; set; }
        public List<EmpAIConference> empconference { get; set; }
    }
}