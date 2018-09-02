using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IARTAutomationApp.Models;
using System.IO;
using IARTAutomationApp.ViewModels;
using ClosedXML.Excel;
using Newtonsoft.Json;
 namespace IARTAutomationApp.Controllers
{
    
    public class HRAdminReportsController : Controller
    {
        IARTDBNEWEntities db = new IARTDBNEWEntities();

        public ActionResult Chart()
        {
            ////Get data from DB, items is list of objects:
            ////1. DisplayText - (string) - chart columns names (equals "labels")
            ////2. Value - (int) - chart values (equals "data")   
            //var items = _Layer.GetData().ToList();

            ////check if data exists
            //if (items.Any())
            //{
            //    string color = "#3c8dbc";
            //    Dataset ds = new Dataset
            //    {
            //        label = string.Empty,
            //        fillColor = color,
            //        pointColor = color,
            //        strokeColor = color
            //    };

            //    var data = items.Select(x => x.Value).ToList();
            //    ds.data.AddRange(data);
            //    model.datasets.Add(ds);

            //    var labels = items.Select(x => x.DisplayText).ToList();
            //    model.labels = labels;
            //}
            var model="null";
            var json = JsonConvert.SerializeObject(model, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });

            return PartialView("_Chart", json);
        }

        public ActionResult chartmorris()
        {
            DateTime today = DateTime.Now.Date;

            var emponleave = (from a in db.LeaveApplications where a.LeaveFromDate>=today && a.LeaveToDate >= today && a.IsApproved==true select a).Count();
            var empdueforleave = (from a in db.LeaveApplications where a.LeaveFromDate > today && a.IsApproved == true select a).Count();

            var emponduty = (from a in db.EmployeeGIs select a).Count();
            ViewBag.emponleave =  emponleave;
            ViewBag.empdueforleave =  empdueforleave;
            emponduty = emponduty - (emponleave + empdueforleave);

            ViewBag.emponduty = emponduty;



            ViewBag.JrStaff = (from a in db.EmployeeGIs where a.Cadre == "Junior" select a).Count();
            ViewBag.SrStaff = (from a in db.EmployeeGIs where a.Cadre == "Senior" select a).Count();
            ViewBag.NyscStaff = (from a in db.EmployeeGIs where a.Cadre == "NYSC Members" select a).Count();
            ViewBag.OthersStaff = (from a in db.EmployeeGIs where a.Cadre == "Others" select a).Count();


            List<DataPoint> dataPoints = new List<DataPoint>{
                new DataPoint(10, 22),
                new DataPoint(20, 36),
                new DataPoint(30, 42),
                new DataPoint(40, 51),
                new DataPoint(50, 46),
            };

            ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints);

            //////////////

            //////////////

            return View();
        }
        public ActionResult GetData()
        {
            DateTime dtRetirement = DateTime.Now.Date;
            List<GraphData> GraphDataList = new List<GraphData>();

            //var user = db.Users.Where(p => p.Email == User.Identity.Name).Single();
            var Requests = db.EmployeeGIs;
            DateTime day = new DateTime();
            int CountPerDay = 0;
            // count of request per day
            foreach (var request in Requests)
            {
                dtRetirement =Convert.ToDateTime( request.DateOfRetirement);
                if (day.Year == dtRetirement.Year && day.Day == dtRetirement.Day)
                {
                    CountPerDay++;
                }
                else
                {
                    // To 2016-12-03 format of date
                    string Date = day.Year + "-" + day.Month + "-" + day.Day;
                    GraphDataList.Add(new GraphData(Date, CountPerDay));
                    CountPerDay = 0;
                    day = dtRetirement;
                }
            }
            // First elem in list is wrong
            GraphDataList.RemoveAt(0);
            return Json(GraphDataList, JsonRequestBehavior.AllowGet);
        }


        public ActionResult EmployeePersonalReport()
        {
            EmployeeAll empall = new EmployeeAll();

            empall.employeegi = (from a in db.EmployeeGIs select a).ToList();
            empall.employeeai = (from a in db.EmployeeAIs select a).ToList();
            empall.employeemi = (from a in db.EmployeeMIs select a).ToList();
            empall.employeesi = (from a in db.EmployeeSIs select a).ToList();
            empall.employeepi = (from a in db.EmployeePIs select a).ToList();
            return View(empall);
        }
        public ActionResult EmployeeCPSsReport()
        {
            EmployeeAll empall = new EmployeeAll();

            empall.employeegi = (from a in db.EmployeeGIs select a).ToList();
            empall.employeeai = (from a in db.EmployeeAIs select a).ToList();
            empall.employeemi = (from a in db.EmployeeMIs select a).ToList();
            empall.employeesi = (from a in db.EmployeeSIs select a).ToList();
            empall.employeepi = (from a in db.EmployeePIs select a).ToList();
            return View(empall);
        }
        public ActionResult EmployeeAcademicReport()
        {
            EmployeeAll empall = new EmployeeAll();

            empall.employeegi = (from a in db.EmployeeGIs select a).ToList();
            empall.employeeai = (from a in db.EmployeeAIs select a).ToList();
            empall.employeemi = (from a in db.EmployeeMIs select a).ToList();
            empall.employeesi = (from a in db.EmployeeSIs select a).ToList();
            empall.employeepi = (from a in db.EmployeePIs select a).ToList();
            return View(empall);
        }

      

        public ActionResult EmployeeStafNominalReport()
        {
            EmployeeAll empall = new EmployeeAll();

            empall.employeegi = (from a in db.EmployeeGIs select a).ToList();
            empall.employeeai = (from a in db.EmployeeAIs select a).ToList();
            empall.employeemi = (from a in db.EmployeeMIs select a).ToList();
            empall.employeesi = (from a in db.EmployeeSIs select a).ToList();
            empall.employeepi = (from a in db.EmployeePIs select a).ToList();
            return View(empall);
        }

        public ActionResult EmployeeMedicalReport()
        {
            EmployeeAll empall = new EmployeeAll();

            empall.employeegi = (from a in db.EmployeeGIs select a).ToList();
            empall.employeeai = (from a in db.EmployeeAIs select a).ToList();
            empall.employeemi = (from a in db.EmployeeMIs select a).ToList();
            empall.employeesi = (from a in db.EmployeeSIs select a).ToList();
            empall.employeepi = (from a in db.EmployeePIs select a).ToList();
            return View(empall);
        }

        public ActionResult EmployeeGeneralReport()
        {
            EmployeeAll empall = new EmployeeAll();

            empall.employeegi = (from a in db.EmployeeGIs select a).ToList();
            empall.employeeai = (from a in db.EmployeeAIs select a).ToList();
            empall.employeemi = (from a in db.EmployeeMIs select a).ToList();
            empall.employeesi = (from a in db.EmployeeSIs select a).ToList();
            empall.employeepi = (from a in db.EmployeePIs select a).ToList();
            return View(empall);
        }
        

        public ActionResult chartchartjs()
        {
            return View();
        }

        public ActionResult EmployeeGIReport()
        {
            return View();
        }
        [HttpPost]
        public ActionResult EmployeeGIReport(int? id)
        {
            IARTDBNEWEntities entities = new IARTDBNEWEntities();
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[18] { new DataColumn("EmplyeeId"),
                                            new DataColumn("Name"),
                                                new DataColumn("Rank"),
                                            new DataColumn("File_No"),
                                                    new DataColumn("Grade_Level"),
                                                       new DataColumn("Step"),
                                                            new DataColumn("Cadre"),
                                                                          new DataColumn("DateOfBirth"),
                                                                               new DataColumn("StateOfOrigin"),
                                                                            new DataColumn("Religion"),
                                                                                    new DataColumn("StationOfDeployment"),
                                                                                       new DataColumn("DateOfRetirement"),
                                                                                        new DataColumn("LastPromotionDate"),
                                                                                          new DataColumn("Unit_Research"),
                                                                                            new DataColumn("Unit_Services"),
                                                                                               new DataColumn("Progrommes"),
                                            new DataColumn("HomeTown"),
                                            new DataColumn("LGA")
            });

            var employees = from emp in db.EmployeeGIs
                            select emp;

            foreach (var emp in employees)
            {
                dt.Rows.Add(emp.EmployeeCode, emp.First_Name, emp.Rank, emp.File_No, emp.Grade_Level, emp.Step, emp.Cadre, emp.DateOfBirth, emp.StateOfOrigin, emp.Religion, emp.StationOfDeployment, emp.DateOfRetirement, emp.LastPromotionDate, emp.Unit_Research, emp.Unit_Services, emp.Home_Town, emp.LGA);
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "EmployeeDetails.xlsx");
                }
            }

        }
        public ActionResult EmployeeStaffNominalReport()
        {
            return View();
        }

        [HttpPost]
        public ActionResult EmployeeStaffNominalReport(int? id)
        {
            IARTDBNEWEntities entities = new IARTDBNEWEntities();
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[20]
            { new DataColumn("File_No"), new DataColumn("EmplyeeId"), new DataColumn("Name"),new DataColumn("Cadre"),

                                                new DataColumn("Rank"), new DataColumn("DateOfBirth"),    new DataColumn("FirstAppointmentLocation"),

                                                      new DataColumn("FirstAppointmentDate"),  new DataColumn("LastPromotionDate"),  new DataColumn("Qualification3"),

                                                         new DataColumn("StateOfOrigin"),  new DataColumn("LGA"), new DataColumn("Sex"),

                                                           new DataColumn("SalaryScale"),  new DataColumn("NhisNo"),    new DataColumn("NhisProvider"),

                                                                   new DataColumn("StationOfDeployment"),   new DataColumn("PFA"),  new DataColumn("MobileNo"), new DataColumn("DateOfRetirement")

            });
            var employees = from a in db.EmployeeGIs
                            from b in db.EmployeePIs
                            from c in db.EmployeeSIs
                            from d in db.EmployeeAIs
                            from e in db.EmployeeMIs
                            where (a.EmployeeCode == b.EmployeeCode) && (c.EmployeeCode == d.EmployeeCode) && (d.EmployeeCode == e.EmployeeCode) && (a.EmployeeCode == e.EmployeeCode)

                            select new { empgi = a, emppi = b, empsi = c, empai = d, empmi = e };

            foreach (var emp in employees)
            {
                dt.Rows.Add(emp.empgi.File_No, emp.empgi.EmployeeCode, emp.empgi.First_Name, emp.empgi.Cadre, emp.empgi.Rank, emp.empgi.DateOfBirth, emp.empgi.FirstAppointmentLocation, emp.empgi.FirstAppointmentDate, emp.empgi.LastPromotionDate, emp.empai.Qualification3, emp.empgi.StateOfOrigin,
                     emp.empgi.LGA, emp.empgi.Sex, emp.empsi.SalaryScale, emp.empmi.NhisNo, emp.empmi.NhisProvider, emp.empgi.StationOfDeployment, emp.empsi.PFA, emp.emppi.MobileNo, emp.empgi.DateOfRetirement
                   );
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "StaffNominalRoll.xlsx");
                }
            }

        }


        public ActionResult EmployeePIReport()
        {
            return View();
        }
        //		S/N	FILE NO	Employee CODE	Title (Mr, Mrs, Miss, DR. etc.)	Surname	First Name	Middle Name	SEX	QUALIFICATION	Date of Birth	Place of Birth	
        //Marital Status	Maiden Name	Mother's Maiden Name	Spouse Name	State of Origin	LGA of Origin	Home town	Religion
        //Blood Group	Genotype	Contact Home Address	LAST PROMOTION	Permanent Home Address	State of Residence	
        //LGA of Residence	Mobile No	Email	FIRST APPT	DATE 0F FIRST APPT	PRESENT APPT	Date of Present Appointment	Confirmation Date	Date of Assumption of Duty	LAST PROMOTION	Job Town
        //Bank Type	Bank Name	Bank Branch	Account Number	Bank Account Name	Pension Name	Pension Pin	Cadre/Job Schedule	
        //Department	Unit	Section	Salary Structure	Ministry/Agency	Grade Level	Step	Establishment/Civil Service No	
        //NHIS NO	NHIS PROVIDER	STATION OF DEPLOYMENT	PFA	DATE OF RETIREMENT

        [HttpPost]
        public ActionResult EmployeePIReport(int? id)
        {
            IARTDBNEWEntities entities = new IARTDBNEWEntities();
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[54] { new DataColumn("File No"), new DataColumn("Employee Code"),
       new DataColumn("Title"),

       new DataColumn("First Name"),
                                                                                                        new DataColumn("MiddleName"),
                                                                                                        new DataColumn("SurName"),

                                                                          new DataColumn("Sex"),
                                                    new DataColumn("Qualification"),
                                                       new DataColumn("Date Of Birth"),
                                                        new DataColumn("Place Of Birth"),
                                                             new DataColumn("Marital Status"),
                                                       new DataColumn("Maiden Name"),
                                                            new DataColumn("Spouse Name"),
                                                                          new DataColumn("State Of Origin"),
                                                                             new DataColumn("LGA Of Origin"),
                                                                             new DataColumn("Home Town"),
                                                                              new DataColumn("Religion"),
                                                                              new DataColumn("Blood Group"),
                                                                              new DataColumn("Geno Type"),
                                                                              new DataColumn("Contact Home Address"),
                                                                              new DataColumn("Last Promotion"),
                                                                              new DataColumn("Permanent Home Address"),
                                                                                new DataColumn("State Of Residence"),
                                                                                        new DataColumn("LGA of Residence")  ,new DataColumn("Mobile No"),new DataColumn("Email"),
                                                                                        new DataColumn("FIRST APPT"),new DataColumn("DATE 0F FIRST APPT"),new DataColumn("PRESENT APPT"),
                new DataColumn("Date of Present Appointment"),new DataColumn("Confirmation Date"),
                                                                                      new DataColumn("Date of Assumption of Duty"),new DataColumn("LAST PROMOTION"),new DataColumn("Job Town"),
        new DataColumn("Bank Type"),new DataColumn("Bank Name"),new DataColumn("Bank Branch"),new DataColumn("Account Number"),new DataColumn("Bank Account Name"),new DataColumn("Pension Name"),new DataColumn("Pension Pin"),new DataColumn("Cadre/Job Schedule")
        ,new DataColumn("Department"),new DataColumn("Unit"),new DataColumn("Section"),new DataColumn("Salary Structure"),new DataColumn("Grade Level"),new DataColumn("Step"),new DataColumn("Establishment/Civil Service No"),
       new DataColumn("NHIS NO"),new DataColumn("NHIS PROVIDER"),new DataColumn("STATION OF DEPLOYMENT"),new DataColumn("PFA"),new DataColumn("DATE OF RETIREMENT"),

             });

            var employees = from a in db.EmployeeGIs
                            from b in db.EmployeePIs
                            from c in db.EmployeeSIs
                            from d in db.EmployeeAIs
                            from e in db.EmployeeMIs
                            where (a.EmployeeCode == b.EmployeeCode) && (c.EmployeeCode == d.EmployeeCode) && (d.EmployeeCode == e.EmployeeCode) && (a.EmployeeCode == e.EmployeeCode)

                            select new { empgi = a, emppi = b, empsi = c, empai = d,empmi=e };

            //		S/N	FILE NO	Employee CODE	Title (Mr, Mrs, Miss, DR. etc.)	Surname	First Name	Middle Name	SEX	QUALIFICATION	Date of Birth	Place of Birth	
            //Marital Status	Maiden Name	Mother's Maiden Name	Spouse Name	State of Origin	LGA of Origin	Home town	Religion
            //Blood Group	Genotype	Contact Home Address	LAST PROMOTION	Permanent Home Address	State of Residence	
            //LGA of Residence	Mobile No	Email	FIRST APPT	DATE 0F FIRST APPT	PRESENT APPT	Date of Present Appointment	Confirmation Date	Date of Assumption of Duty	LAST PROMOTION	Job Town
            //Bank Type	Bank Name	Bank Branch	Account Number	Bank Account Name	Pension Name	Pension Pin	Cadre/Job Schedule	
            //Department	Unit	Section	Salary Structure	Ministry/Agency	Grade Level	Step	Establishment/Civil Service No	
            //NHIS NO	NHIS PROVIDER	STATION OF DEPLOYMENT	PFA	DATE OF RETIREMENT
            foreach (var emp in employees)
            {
                dt.Rows.Add(emp.empgi.File_No, emp.empgi.EmployeeCode, emp.empgi.Title,emp.empgi.Surname, emp.empgi.First_Name,emp.empgi.Middle_Name,emp.empgi.Sex,emp.empai.Qualification3,
                    emp.empgi.DateOfBirth,emp.empgi.PlaceOfBirth,emp.empgi.Marital_Status,emp.empgi.Maiden_Name,emp.empgi.Spouse_Name,emp.empgi.StateOfOrigin,emp.empgi.LGA,emp.empgi.Home_Town,emp.empgi.Religion, 
                    emp.empmi.BloodGroup,emp.empmi.BloodGenotype,emp.empgi.ContactHomeAddress,emp.empgi.LastPromotionDate,emp.emppi.PermanentAddress,emp.empgi.StateOfOrigin,
                    emp.empsi.BankType,emp.empsi.NameOfBanks,emp.empsi.BankBranch,emp.empsi.AccountNumber,emp.empsi.AccountName,emp.empsi.PFA,emp.empsi.RSAPinNo,emp.empgi.Cadre,
                    emp.empgi.Rank,emp.empgi.Unit_Research+","+emp.empgi.Unit_Services,emp.empgi.Section,emp.empsi.SalaryScale, emp.empgi.Grade_Level, emp.empgi.Step, "N/A", 
                    emp.empmi.NhisNo,emp.empmi.NhisProvider,emp.empgi.StationOfDeployment,emp.empsi.PFA,emp.empgi.DateOfRetirement
                  );
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.ShowRowColHeaders = true;
                wb.Worksheets.Add(dt);

                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "EmployeePersonalInfoReport.xlsx");
                }
            }

        }

        //		S/N	FILE NO	Employee CODE	Title (Mr, Mrs, Miss, DR. etc.)	Surname	First Name	Middle Name	SEX	NHIS NO	NHIS PROVIDER	Blood Group	Genotype																																													

        public ActionResult EmployeeMIReport()
        {
            return View();
        }

        [HttpPost]
        public ActionResult EmployeeMIReport(int? id)
        {
            IARTDBNEWEntities entities = new IARTDBNEWEntities();
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[11] {new DataColumn("File No"), new DataColumn("Employee Code"),
                                                            new DataColumn("Title"),

                                            new DataColumn("First Name"),
                                                                                                        new DataColumn("MiddleName"),
                                                                                                        new DataColumn("SurName"),

                                                                          new DataColumn("Sex"),
                                                                               new DataColumn("NHIS No."),
                                                                            new DataColumn("NHIS Provider"),
                                                                                    new DataColumn("Blood Group"),
                                                                                       new DataColumn("Genotype")
            });

            var employees = from a in db.EmployeeGIs
                            from b in db.EmployeePIs
                            from c in db.EmployeeSIs
                            from d in db.EmployeeMIs
                            where (a.EmployeeCode==b.EmployeeCode) && (c.EmployeeCode==d.EmployeeCode)  && (a.EmployeeCode== d.EmployeeCode)
                            select new { empgi = a, emppi = b, empsi = c, empmi = d };

            foreach (var emp in employees)
            {
                dt.Rows.Add(emp.empgi.File_No, emp.empgi.EmployeeCode, emp.empgi.Title, emp.empgi.First_Name, emp.empgi.Middle_Name, emp.empgi.Surname, emp.empgi.Sex, emp.empmi.NhisNo, emp.empmi.NhisProvider, emp.empmi.BloodGroup, emp.empmi.BloodGenotype);
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.ShowRowColHeaders = true;
                wb.Worksheets.Add(dt);

                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "EmployeeMedicalRecordReport.xlsx");
                }
            }


        }
        //		S/N	FILE NO	Employee CODE	Title (Mr, Mrs, Miss, DR. etc.)	Surname	First Name	Middle Name	SEX	Schools Attended	QUALIFICATIONS E.g	Name of Professional Trainings Attended	year Attended																																													

        public ActionResult EmployeeAIReport()
        {
            return View();
        }
        [HttpPost]
        public ActionResult EmployeeAIReport(int? id)
        {
            IARTDBNEWEntities entities = new IARTDBNEWEntities();
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[10] {new DataColumn("File No"), new DataColumn("Employee Code"),
                                                            new DataColumn("Title"),

                                            new DataColumn("First Name"),
                                                                                                        new DataColumn("MiddleName"),
                                                                                                        new DataColumn("SurName"),

                                                                          new DataColumn("Sex"),
                                                                               new DataColumn("Institute Attended"),
                                                                            new DataColumn("Qualification"),
                                                                                    new DataColumn("Year Of Graduation")
             });

            var employees = from a in db.EmployeeGIs
                            from b in db.EmployeeAIs
                            where a.EmployeeCode==b.EmployeeCode

                            select new { empgi = a, empai = b };

            foreach (var emp in employees)
            {
                dt.Rows.Add(emp.empgi.File_No, emp.empgi.EmployeeCode, emp.empgi.Title, emp.empgi.First_Name, emp.empgi.Middle_Name, emp.empgi.Surname, emp.empgi.Sex, emp.empai.InstitutionAttended1 + "," + emp.empai.InstitutionAttended2 + "," + emp.empai.InstitutionAttended3
                    , emp.empai.Qualification1 + "," + emp.empai.Qualification2 + "," + emp.empai.Qualification3, emp.empai.YearOfGraduation1 + "," + emp.empai.YearOfGraduation2 + "," + emp.empai.YearOfGraduation3);
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.ShowRowColHeaders = true;
                wb.Worksheets.Add(dt);

                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "EmployeeAcademicReport.xlsx");
                }
            }


        }


        public ActionResult EmployeeCPSReport()
        {
            return View();
        }

        //  S/N FILE NO Employee CODE Title(Mr, Mrs, Miss, DR.etc.) Surname First Name Middle Name SEX Date of Birth Date of Confirmation    Date of Retirement Designation at Retirement   Salary Level    Date of Retirement PFA PIN NO  TEL NO  REMARK
        [HttpPost]
        public ActionResult EmployeeCPSReport(int? id)
        {
            IARTDBNEWEntities entities = new IARTDBNEWEntities();
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[18] {new DataColumn("File No"), new DataColumn("Employee Code"),
                                                            new DataColumn("Title"),

                                            new DataColumn("First Name"),
                                                                                                        new DataColumn("MiddleName"),
                                                                                                        new DataColumn("SurName"),

                                                                          new DataColumn("Sex"),
                                                                               new DataColumn("Date Of Birth"),
                                                                            new DataColumn("Date Of Confirmation"),
                                                                                    new DataColumn("Date of Retirement"),
                                                                                    new DataColumn("Designation at Retirement"),

                new DataColumn("Salary"), new DataColumn("Level"),  new DataColumn("Date Of Retirement"), new DataColumn("PFA"), new DataColumn("PIN No."), new DataColumn("TEL No."), new DataColumn("Remark")


             });

            var employees = from a in db.EmployeeGIs
                            from b in db.EmployeePIs
                            from c in db.EmployeeSIs
                            where (a.EmployeeCode==b.EmployeeCode) && (b.EmployeeCode==c.EmployeeCode)
                            select new { empgi = a, emppi = b.MobileNo, empsi = c };

            foreach (var emp in employees)
            {
                dt.Rows.Add(emp.empgi.File_No, emp.empgi.EmployeeCode, emp.empgi.Title, emp.empgi.First_Name, emp.empgi.Middle_Name, emp.empgi.Surname, emp.empgi.Sex, emp.empgi.DateOfBirth, emp.empgi.ConfirmationDate, emp.empgi.DateOfRetirement,
                    emp.empgi.Rank, emp.empsi.SalaryScale, emp.empgi.Grade_Level, emp.empgi.DateOfRetirement, emp.empsi.PFA, emp.empsi.RSAPinNo, emp.emppi, ""
                    );
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.ShowRowColHeaders = true;
                wb.Worksheets.Add(dt);

                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "EmployeeCPSReport.xlsx");
                }
            }


        }


        // GET: HRAdminReports
        public ActionResult Index()
        {
            return View();
        }

        // GET: HRAdminReports/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: HRAdminReports/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HRAdminReports/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: HRAdminReports/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: HRAdminReports/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: HRAdminReports/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: HRAdminReports/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
