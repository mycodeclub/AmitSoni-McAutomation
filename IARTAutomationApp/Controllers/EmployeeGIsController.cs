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

using PagedList;
namespace IARTAutomationApp.Controllers
{

    public class EmployeeGIsController : Controller
    {
        IARTDBNEWEntities db = new IARTDBNEWEntities();


        ////GET: EmployeeGIs
        public ActionResult Index()
        {
            var user = (UserMaster)Session["User"];
            try
            {
                var employees = db.EmployeeGIs.Where(emp => emp.CustomerId == user.CustomerId && emp.EmployeeCode != user.EmployeeCode).ToList();
                DateTime dttoday = DateTime.Now.Date;
                var Role = (from a in db.UserMasters where a.EmployeeCode == user.EmployeeCode select a.RoleId).FirstOrDefault();
                var RoleName = (from b in db.RoleMasters where b.RoleId == Role select b.RoleName).FirstOrDefault();
                if (RoleName == "Super Admin" || RoleName == "Admin" || RoleName == "Super Level Admin" || RoleName == "HR Admin")
                {
                    ViewBag.NoOfStaff = employees.Count();
                    ViewBag.NoofNewEmp = (from e in employees where e.FirstAppointmentDate >= DateTime.Now.Date.AddDays(-30) select e).Count();
                    ViewBag.NeartoRetirement = (from e in employees where e.DateOfRetirement <= DateTime.Now.Date.AddMonths(6) select e).Count();
                    ViewBag.onleave = (from a in db.LeaveApplications where a.AppDate == dttoday.Date select a.LeaveAccId).ToList().Count();
                    return View(employees);
                }
                else
                {
                    var employeeGIs = db.EmployeeGIs.Where(a => a.EmployeeCode == user.EmployeeCode).ToList();
                    return View(employeeGIs.ToList());
                }
            }
            catch
            {
                return RedirectToAction("AdminLogin", "Login");

            }
        }
        public ActionResult UserIndex()
        {

            try
            {


                int empcode = Convert.ToInt32(@Session["employeecode"]);


                var employeeGIs = db.EmployeeGIs.Where(a => a.EmployeeCode == empcode).ToList();
                return View(employeeGIs.ToList());

                //if(     User.Identity.Name)



            }
            catch (Exception ext)
            {
                return RedirectToAction("AdminLogin", "Login");
            }
            return View();
        }
        public ActionResult GetLGAList(string stateId)
        {
            List<SelectListItem> licity = new List<SelectListItem>();

            try
            {
                var stateIDs = (db.StateMasters.Where(x => x.State == stateId).Select(x => x.Id).FirstOrDefault());
                int stateID = Convert.ToInt32(stateIDs.ToString());
                var City = db.CityMasters.Where(x => x.StateId == stateID).ToList();

                licity.Add(new SelectListItem { Text = "--Select LGA--", Value = "0" });
                if (City != null)
                {
                    foreach (var l in City)
                    {
                        licity.Add(new SelectListItem { Text = l.City, Value = l.City.ToString() });

                    }
                }
                return Json(new SelectList(licity, "Value", "Text", JsonRequestBehavior.AllowGet));

            }
            catch (Exception ext)
            {
                return Json(new SelectList(licity, "Value", "Text", JsonRequestBehavior.AllowGet));

            }
        }
        // GET: EmployeeGIs/Details/5
        public ActionResult Details(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeGI employeeGI = db.EmployeeGIs.Find(id);
            if (employeeGI == null)
            {
                return HttpNotFound();
            }
            return View(employeeGI);
        }
        // GET: EmployeeGIs/Create
        public ActionResult Create()
        {
            try
            {
                EmployeeGI empgi = new EmployeeGI();
                empgi.DateOfBirth = DateTime.Now.Date.AddYears(-18).Date;
                ViewBag.EmployeeCode = new SelectList(db.EmployeeGIs, "EmployeeCode", "EmployeeCode");
                List<SelectListItem> File_Nos = new List<SelectListItem>();
                for (int i = 1; i <= 111; i++)
                {
                    File_Nos.Add(new SelectListItem { Text = i.ToString(), Value = i.ToString() });
                }
                var user = (UserMaster)Session["User"];
                ViewBag.Ranks = new SelectList(db.RankMasters.Where(item => item.CustomerId == user.CustomerId), "RankName", "RankName");
                ViewBag.File_Nos = new SelectList(File_Nos, "Value", "Text");
                ViewBag.LGAs = new SelectList(db.CityMasters, "City", "City");
                ViewBag.StateOfOrigins = new SelectList(db.StateMasters, "State", "State");
                ViewBag.cadres = new SelectList(db.CadreMasters.Where(item => item.CustomerId == user.CustomerId), "CadreName", "CadreName");
                ViewBag.Programmess = new SelectList(db.ProgrammeMasters.Where(item => item.CustomerId == user.CustomerId), "ProgrammeName", "ProgrammeName");
                ViewBag.Unit_Researchs = new SelectList(db.UnitResearchMasters.Where(item => item.CustomerId == user.CustomerId), "UnitResearchName", "UnitResearchName");
                ViewBag.Unit_Servicess = new SelectList(db.UnitServicesMasters.Where(item => item.CustomerId == user.CustomerId), "UnitServicesName", "UnitServicesName");
                ViewBag.StationOfDeployments = new SelectList(db.StationMasters.Where(item => item.CustomerId == user.CustomerId), "StationName", "StationName");
                ViewBag.Sections = new SelectList(db.SectionMasters.Where(item => item.CustomerId == user.CustomerId), "SectionName", "SectionName");
                return View(empgi);
            }
            catch (Exception ext)
            {
                return null;
            }
        }
        // POST: EmployeeGIs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CustomerId,EmployeeGIId,EmployeeCode,Rank,File_No,Grade_Level,Step,Cadre,Title,First_Name,Middle_Name,Surname,Sex,DateOfBirth,PlaceOfBirth,Marital_Status,Maiden_Name,Spouse_Name,StateOfOrigin,LGA,Home_Town,Religion,ContactHomeAddress,FirstAppointmentDate,FirstAppointmentLocation,ConfirmationDate,DateOfRetirement,LastPromotionDate,Programmes,Unit_Services,Unit_Research,Section,LeaveDays,Leave_fromDate,Leave_ToDate,,EmployeePhotoImage,StationOfDeployment,IsDeleted,CreatedDate")] EmployeeGI employeeGI)
        {
            var user = (UserMaster)Session["User"];
            var isAlready = (from a in db.EmployeeGIs where a.EmployeeCode == employeeGI.EmployeeCode select a.EmployeeCode).Count();
            if (isAlready == 0)
            {
                try
                {
                    if (employeeGI.Marital_Status != "Married")
                        employeeGI.Spouse_Name = "N/A";
                    List<SelectListItem> File_Nos = new List<SelectListItem>();
                    File_Nos.Add(new SelectListItem { Text = "Select", Value = "0" });
                    for (int i = 1; i <= 111; i++)
                    {
                        File_Nos.Add(new SelectListItem { Text = i.ToString(), Value = i.ToString() });
                    }
                    ViewBag.File_No = new SelectList(File_Nos, "Value", "Text");
                    employeeGI.CreatedDate = DateTime.Now.Date;
                    employeeGI.IsDeleted = false;
                    ViewBag.Ranks = new SelectList(db.RankMasters, "RankName", "RankName");
                    ViewBag.LGAs = new SelectList(db.CityMasters, "City", "City");
                    ViewBag.StateOfOrigins = new SelectList(db.StateMasters, "State", "State");
                    ViewBag.cadres = new SelectList(db.CadreMasters, "CadreName", "CadreName");
                    ViewBag.Programmess = new SelectList(db.ProgrammeMasters, "ProgrammeName", "ProgrammeName");
                    ViewBag.Unit_Researchs = new SelectList(db.UnitResearchMasters, "UnitResearchName", "UnitResearchName");
                    ViewBag.Unit_Servicess = new SelectList(db.UnitServicesMasters, "UnitServicesName", "UnitServicesName");
                    ViewBag.StationOfDeployments = new SelectList(db.StationMasters, "StationName", "StationName");
                    ViewBag.Sections = new SelectList(db.SectionMasters, "SectionName", "SectionName");
                    if (employeeGI.EmployeePhotoImage.FileName != null && employeeGI.EmployeePhotoImage.ContentLength > 0)
                    {
                        var uploadDir = "~/uploads";
                        var imagePath = Path.Combine(Server.MapPath(uploadDir), employeeGI.EmployeePhotoImage.FileName.ToString());
                        var imageUrl = Path.Combine(uploadDir, employeeGI.EmployeePhotoImage.FileName.ToString());
                        employeeGI.EmployeePhoto = employeeGI.EmployeePhotoImage.FileName.ToString();
                        employeeGI.EmployeePhotoImage.SaveAs(imagePath);

                    }
                    employeeGI.File_No = employeeGI.File_No;
                    employeeGI.EmployeeGIId = employeeGI.EmployeeCode;
                    employeeGI.CustomerId = user.CustomerId;
                    db.EmployeeGIs.Add(employeeGI);
                    db.SaveChanges();
                    TempData["successmsg"] = "Record is Successfully added";
                    TempData["msg"] = "";
                    int empcode = Convert.ToInt32(@Session["employeecode"]);
                    DateTime dttoday = DateTime.Now.Date;
                    var Role = (from a in db.UserMasters where a.EmployeeCode == empcode select a.RoleId).FirstOrDefault();
                    var RoleName = (from b in db.RoleMasters where b.RoleId == Role select b.RoleName).FirstOrDefault();
                    return RedirectToAction("Index");
                }
                catch (Exception ext)
                {
                    TempData["successmsg"] = "";
                    TempData["msg"] = "Record is not added,Try again";

                }
                ViewBag.EmployeeCode = new SelectList(db.EmployeeGIs, "EmployeeCode", "EmployeeCode", employeeGI.EmployeeCode);
                return View(employeeGI);
            }
            else
            {
                List<SelectListItem> File_Nos = new List<SelectListItem>();
                File_Nos.Add(new SelectListItem { Text = "Select", Value = "0" });
                for (int i = 1; i <= 111; i++)
                {
                    File_Nos.Add(new SelectListItem { Text = i.ToString(), Value = i.ToString() });
                }
                ViewBag.Ranks = new SelectList(db.RankMasters.Where(item => item.CustomerId == user.CustomerId), "RankName", "RankName");
                ViewBag.File_Nos = new SelectList(File_Nos, "Value", "Text");
                ViewBag.LGAs = new SelectList(db.CityMasters, "City", "City");
                ViewBag.StateOfOrigins = new SelectList(db.StateMasters, "State", "State");
                ViewBag.cadres = new SelectList(db.CadreMasters.Where(item => item.CustomerId == user.CustomerId), "CadreName", "CadreName");
                ViewBag.Programmess = new SelectList(db.ProgrammeMasters.Where(item => item.CustomerId == user.CustomerId), "ProgrammeName", "ProgrammeName");
                ViewBag.Unit_Researchs = new SelectList(db.UnitResearchMasters.Where(item => item.CustomerId == user.CustomerId), "UnitResearchName", "UnitResearchName");
                ViewBag.Unit_Servicess = new SelectList(db.UnitServicesMasters.Where(item => item.CustomerId == user.CustomerId), "UnitServicesName", "UnitServicesName");
                ViewBag.StationOfDeployments = new SelectList(db.StationMasters.Where(item => item.CustomerId == user.CustomerId), "StationName", "StationName");
                ViewBag.Sections = new SelectList(db.SectionMasters.Where(item => item.CustomerId == user.CustomerId), "SectionName", "SectionName");


                TempData["successmsg"] = "";
                TempData["msg"] = "This Employee Record is already exist";
                return View(employeeGI);
            }
        }

        // GET: EmployeeGIs/Edit/5
        public ActionResult Edit(int? id)
        {
            EmployeeGI employeeGI = db.EmployeeGIs.Find(id);
            List<SelectListItem> File_Nos = new List<SelectListItem>();
            File_Nos.Add(new SelectListItem { Text = "Select", Value = "0" });
            for (int i = 1; i <= 200; i++) File_Nos.Add(new SelectListItem { Text = i.ToString(), Value = i.ToString() });
            List<SelectListItem> Titles = new List<SelectListItem>();
            Titles.Add(new SelectListItem { Text = "Mr", Value = "0" });
            Titles.Add(new SelectListItem { Text = "Mrs", Value = "1" });
            Titles.Add(new SelectListItem { Text = "Miss", Value = "2" });
            Titles.Add(new SelectListItem { Text = "Dr", Value = "3" });
            Titles.Add(new SelectListItem { Text = "Prof", Value = "4" });
            ViewBag.Titles = new SelectList(Titles, "Text", "Text");
            List<SelectListItem> Steps = new List<SelectListItem>();
            Steps.Add(new SelectListItem { Text = "Select", Value = "0" });
            for (int i = 1; i <= 15; i++) Steps.Add(new SelectListItem { Text = i.ToString(), Value = i.ToString() });
            List<SelectListItem> Grades = new List<SelectListItem>();
            Grades.Add(new SelectListItem { Text = "Select", Value = "0" });
            for (int i = 1; i <= 15; i++) Grades.Add(new SelectListItem { Text = i.ToString(), Value = i.ToString() });
            ViewBag.Steps = new SelectList(Steps, "Value", "Text");
            ViewBag.Grade_Levels = new SelectList(Grades, "Value", "Text");
            ViewBag.File_Noa = new SelectList(File_Nos, "Value", "Text");
            ViewBag.Ranks = new SelectList(db.RankMasters, "RankName", "RankName");
            ViewBag.LGAs = new SelectList(db.CityMasters, "City", "City");
            ViewBag.StateOfOrigins = new SelectList(db.StateMasters, "State", "State");
            ViewBag.cadres = new SelectList(db.CadreMasters, "CadreName", "CadreName");
            ViewBag.Programmess = new SelectList(db.ProgrammeMasters, "ProgrammeName", "ProgrammeName");
            ViewBag.Unit_Researchs = new SelectList(db.UnitResearchMasters, "UnitResearchName", "UnitResearchName");
            ViewBag.Unit_Servicess = new SelectList(db.UnitServicesMasters, "UnitServicesName", "UnitServicesName");
            ViewBag.StationOfDeployments = new SelectList(db.StationMasters, "StationName", "StationName");
            ViewBag.Sections = new SelectList(db.SectionMasters, "SectionName", "SectionName");
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            if (employeeGI == null) return HttpNotFound();
            ViewBag.EmployeeCode = new SelectList(db.EmployeeGIs, "EmployeeCode", "EmployeeCode", employeeGI.EmployeeCode);
            return View(employeeGI);
        }

        // POST: EmployeeGIs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustomerId,EmployeeGIId,EmployeeCode,Rank,File_No,Grade_Level,Step,Cadre,Title,First_Name,Middle_Name,Surname,Sex,DateOfBirth,PlaceOfBirth,Marital_Status,Maiden_Name,Spouse_Name,StateOfOrigin,LGA,Home_Town,Religion,ContactHomeAddress,FirstAppointmentDate,FirstAppointmentLocation,ConfirmationDate,DateOfRetirement,LastPromotionDate,Programmes,Unit_Services,Unit_Research,Section,LeaveDays,Leave_fromDate,Leave_ToDate,EmployeePhoto,EmployeePhotoImage,StationOfDeployment,IsDeleted,CreatedDate")] EmployeeGI employeeGI)
        {
            try
            {


                //if (ModelState.IsValid)
                //{
                if (employeeGI.EmployeePhotoImage != null && employeeGI.EmployeePhotoImage.ContentLength > 0)
                {
                    var uploadDir = "~/uploads";
                    var imagePath = Path.Combine(Server.MapPath(uploadDir), employeeGI.EmployeePhotoImage.FileName.ToString());
                    var imageUrl = Path.Combine(uploadDir, employeeGI.EmployeePhotoImage.FileName.ToString());
                    employeeGI.EmployeePhoto = employeeGI.EmployeePhotoImage.FileName.ToString();
                    employeeGI.EmployeePhotoImage.SaveAs(imagePath);

                }
                if (employeeGI.Marital_Status != "Married")
                    employeeGI.Spouse_Name = "N/A";


                employeeGI.Leave_fromDate = DateTime.Now.Date;
                employeeGI.Leave_ToDate = DateTime.Now.Date;
                TimeSpan daydiff = Convert.ToDateTime(employeeGI.Leave_ToDate) - Convert.ToDateTime(employeeGI.Leave_fromDate);

                employeeGI.LeaveDays = daydiff.Days;
                employeeGI.CreatedDate = DateTime.Now.Date;
                employeeGI.IsDeleted = false;
                db.Entry(employeeGI).State = EntityState.Modified;
                db.SaveChanges();
                TempData["successmsg"] = "Record is Successfully Updated";
                TempData["msg"] = "";
                int empcode = Convert.ToInt32(@Session["employeecode"]);

                DateTime dttoday = DateTime.Now.Date;
                var Role = (from a in db.UserMasters where a.EmployeeCode == empcode select a.RoleId).FirstOrDefault();
                var RoleName = (from b in db.RoleMasters where b.RoleId == Role select b.RoleName).FirstOrDefault();
                //if (RoleName == "Admin" || RoleName == "Super Admin")
                //{
                return RedirectToAction("Index");
                //}
                //else if (RoleName == "Mid Level Admin")
                //{
                //    return RedirectToAction("MidLevelIndex");
                //}
                //else if (RoleName == "Low Level Admin")
                //{
                //    return RedirectToAction("MidLevelIndex");
                //}
                //else
                //{
                //    return RedirectToAction("UserIndex");
                //}
            }
            catch (Exception ext)
            {
                TempData["successmsg"] = "";

                TempData["msg"] = "Record is not Updated,Try again";

            }
            //}
            //ViewBag.EmployeeCode = new SelectList(db.EmployeeGIs, "EmployeeCode", "EmployeeCode", employeeGI.EmployeeCode);
            return RedirectToAction("Index");
        }



        #region MidLevel

        public ActionResult MidLevelIndex()
        {
            try
            {
                int empcode = Convert.ToInt32(@Session["employeecode"]);
                {
                    DateTime dttoday = DateTime.Now.Date;
                    var Role = (from a in db.UserMasters where a.EmployeeCode == empcode select a.RoleId).FirstOrDefault();
                    var RoleName = (from b in db.RoleMasters where b.RoleId == Role select b.RoleName).FirstOrDefault();
                    if (RoleName == "Mid Level Admin" || RoleName == "Low Level Admin")
                    {
                        var NoofEmp = (from a in db.EmployeeGIs select a).ToList().Count();
                        ViewBag.NoOfStaff = NoofEmp;
                        DateTime newDateRange = DateTime.Now.Date.AddDays(-30);
                        var NoofNewEmp = (from b in db.EmployeeGIs where b.FirstAppointmentDate >= newDateRange select b).Count();
                        ViewBag.NoofNewEmp = NoofNewEmp;
                        DateTime extDateRange = DateTime.Now.Date.AddMonths(6);
                        var NearToRetirementCount = (from a in db.EmployeeGIs where a.DateOfRetirement <= extDateRange select a).ToList().Count();
                        ViewBag.NeartoRetirement = NearToRetirementCount;

                        ViewBag.onleave = (from a in db.LeaveApplications where a.AppDate == dttoday.Date select a.LeaveAccId).ToList().Count();




                        var employeeGIs = db.EmployeeGIs.Where(a => a.EmployeeCode == empcode).ToList();
                        return View(employeeGIs.ToList());
                    }
                    else
                    {

                        var employeeGIs = db.EmployeeGIs.Where(a => a.EmployeeCode == empcode).ToList();
                        return View(employeeGIs.ToList());
                    }
                }


                //if(     User.Identity.Name)


            }
            catch (Exception ext)
            {
                return RedirectToAction("AdminLogin", "Login");

            }
            return View();
        }
        // GET: EmployeeGIs/Create
        public ActionResult MidLevelCreate()
        {
            try
            {
                EmployeeGI empgi = new EmployeeGI();
                //empgi.DateOfBirth = DateTime.Now.Date.AddYears(-18).Date;

                ViewBag.EmployeeCode = new SelectList(db.EmployeeGIs, "EmployeeCode", "EmployeeCode");

                List<SelectListItem> File_Nos = new List<SelectListItem>();
                File_Nos.Add(new SelectListItem { Text = "Select", Value = "0" });
                for (int i = 1; i <= 111; i++)
                {
                    File_Nos.Add(new SelectListItem { Text = i.ToString(), Value = i.ToString() });
                }

                ViewBag.File_Nos = new SelectList(File_Nos, "Value", "Text");

                ///////
                ViewBag.Ranks = new SelectList(db.RankMasters, "RankName", "RankName");
                ViewBag.LGAs = new SelectList(db.CityMasters, "City", "City");
                ViewBag.StateOfOrigins = new SelectList(db.StateMasters, "State", "State");
                ViewBag.cadres = new SelectList(db.CadreMasters, "CadreName", "CadreName");
                ViewBag.Programmess = new SelectList(db.ProgrammeMasters, "ProgrammeName", "ProgrammeName");
                ViewBag.Unit_Researchs = new SelectList(db.UnitResearchMasters, "UnitResearchName", "UnitResearchName");
                ViewBag.Unit_Servicess = new SelectList(db.UnitServicesMasters, "UnitServicesName", "UnitServicesName");
                ViewBag.StationOfDeployments = new SelectList(db.StationMasters, "StationName", "StationName");
                ViewBag.Sections = new SelectList(db.SectionMasters, "SectionName", "SectionName");



                //List<RankMaster> Ranklist = new List<RankMaster>();
                //Ranklist = (from a in db.RankMasters select a).ToList();
                ////ViewBag.CountryList = CountryList;
                //ViewBag.Rank = new SelectList(Ranklist, "RankId", "RankName");

                ///////

                //List<CityMaster> CityList = new List<CityMaster>();
                //CityList = (from a in db.CityMasters select a).ToList();
                ////ViewBag.CountryList = CountryList;
                //ViewBag.LGA = new SelectList(CityList, "Id", "City");
                ///////
                //List<StateMaster> StateList = new List<StateMaster>();
                //StateList = (from State in db.StateMasters select State).ToList();
                ////ViewBag.CountryList = CountryList;
                //ViewBag.StateOfOrigin = new SelectList(StateList, "Id", "State");
                ///////

                //List<CadreMaster> cadrelist = new List<CadreMaster>();
                //cadrelist = (from a in db.CadreMasters select a).ToList();
                ////ViewBag.CountryList = CountryList;
                //ViewBag.cadre = new SelectList(cadrelist, "CadreId", "CadreName");

                ///////

                //List<ProgrammeMaster> Programlist = new List<ProgrammeMaster>();
                //Programlist = (from a in db.ProgrammeMasters select a).ToList();
                ////ViewBag.CountryList = CountryList;
                //ViewBag.Programmes = new SelectList(Programlist, "ProgrammeId", "ProgrammeName");

                ///////

                //List<UnitResearchMaster> UnitResearchlist = new List<UnitResearchMaster>();
                //UnitResearchlist = (from a in db.UnitResearchMasters select a).ToList();
                ////ViewBag.CountryList = CountryList;
                //ViewBag.Unit_Research = new SelectList(UnitResearchlist, "UnitResearchId", "UnitResearchName");

                ///////

                //List<UnitServicesMaster> UnitServicelist = new List<UnitServicesMaster>();
                //UnitServicelist = (from a in db.UnitServicesMasters select a).ToList();
                ////ViewBag.CountryList = CountryList;
                //ViewBag.Unit_Services = new SelectList(UnitServicelist, "UnitServicesId", "UnitServicesName");

                ///////

                //List<StationMaster> Stationlist = new List<StationMaster>();
                //Stationlist = (from a in db.StationMasters select a).ToList();
                ////ViewBag.CountryList = CountryList;
                //ViewBag.StationOfDeployment = new SelectList(Stationlist, "StationId", "StationName");


                ///////

                //List<SectionMaster> Sectionlist = new List<SectionMaster>();
                //Sectionlist = (from a in db.SectionMasters select a).ToList();
                ////ViewBag.CountryList = CountryList;
                //ViewBag.Section = new SelectList(Sectionlist, "SectionId", "SectionName");


                return View(empgi);
            }
            catch (Exception ext)
            {
                return null;
            }
        }



        // POST: EmployeeGIs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult MidLevelCreate([Bind(Include = "EmployeeGIId,EmployeeCode,Rank,File_No,Grade_Level,Step,Cadre,Title,First_Name,Middle_Name,Surname,Sex,DateOfBirth,PlaceOfBirth,Marital_Status,Maiden_Name,Spouse_Name,StateOfOrigin,LGA,Home_Town,Religion,ContactHomeAddress,FirstAppointmentDate,FirstAppointmentLocation,ConfirmationDate,DateOfRetirement,LastPromotionDate,Programmes,Unit_Services,Unit_Research,Section,LeaveDays,Leave_fromDate,Leave_ToDate,,EmployeePhotoImage,StationOfDeployment,IsDeleted,CreatedDate")] EmployeeGI employeeGI)
        {
            try
            {
                if (employeeGI.Marital_Status != "Married")
                    employeeGI.Spouse_Name = "N/A";

                var list = new List<int>();
                for (int i = 1; i <= 111; i++)
                {
                    list.Add(i);
                }
                List<SelectListItem> File_Nos = new List<SelectListItem>();
                File_Nos.Add(new SelectListItem { Text = "Select", Value = "0" });
                for (int i = 1; i <= 111; i++)
                {
                    File_Nos.Add(new SelectListItem { Text = i.ToString(), Value = i.ToString() });
                }

                ViewBag.File_No = new SelectList(File_Nos, "Value", "Text");
                employeeGI.CreatedDate = DateTime.Now.Date;
                employeeGI.IsDeleted = false;
                ViewBag.Ranks = new SelectList(db.RankMasters, "RankName", "RankName");
                ViewBag.LGAs = new SelectList(db.CityMasters, "City", "City");
                ViewBag.StateOfOrigins = new SelectList(db.StateMasters, "State", "State");
                ViewBag.cadres = new SelectList(db.CadreMasters, "CadreName", "CadreName");
                ViewBag.Programmess = new SelectList(db.ProgrammeMasters, "ProgrammeName", "ProgrammeName");
                ViewBag.Unit_Researchs = new SelectList(db.UnitResearchMasters, "UnitResearchName", "UnitResearchName");
                ViewBag.Unit_Servicess = new SelectList(db.UnitServicesMasters, "UnitServicesName", "UnitServicesName");
                ViewBag.StationOfDeployments = new SelectList(db.StationMasters, "StationName", "StationName");
                ViewBag.Sections = new SelectList(db.SectionMasters, "SectionName", "SectionName");

                //if (ModelState.IsValid)
                //{

                if (employeeGI.EmployeePhotoImage.FileName != null && employeeGI.EmployeePhotoImage.ContentLength > 0)
                {
                    var uploadDir = "~/uploads";
                    var imagePath = Path.Combine(Server.MapPath(uploadDir), employeeGI.EmployeePhotoImage.FileName.ToString());
                    var imageUrl = Path.Combine(uploadDir, employeeGI.EmployeePhotoImage.FileName.ToString());
                    employeeGI.EmployeePhoto = employeeGI.EmployeePhotoImage.FileName.ToString();
                    employeeGI.EmployeePhotoImage.SaveAs(imagePath);

                }



                employeeGI.File_No = employeeGI.File_No;

                employeeGI.EmployeeGIId = employeeGI.EmployeeCode;
                db.EmployeeGIs.Add(employeeGI);
                db.SaveChanges();
                TempData["successmsg"] = "Record is Successfully added";
                TempData["msg"] = "";

                return RedirectToAction("MidLevelIndex");
                //int empcode = Convert.ToInt32(@Session["employeecode"]);

                //DateTime dttoday = DateTime.Now.Date;
                //var Role = (from a in db.UserMasters where a.EmployeeCode == empcode select a.RoleId).FirstOrDefault();
                //var RoleName = (from b in db.RoleMasters where b.RoleId == Role select b.RoleName).FirstOrDefault();
                //if (RoleName == "Admin" || RoleName == "Super Admin")
                //{
                //    return RedirectToAction("Index");
                //}
                //else if (RoleName == "Mid Level Admin")
                //{
                //    return RedirectToAction("MidLevelIndex");
                //}
                //else if (RoleName == "Low Level Admin")
                //{
                //    return RedirectToAction("MidLevelIndex");
                //}
                //else
                //{
                //    return RedirectToAction("UserIndex");
                //}


            }
            catch (Exception ext)
            {
                TempData["successmsg"] = "";
                TempData["msg"] = "Record is not added,Try again";

            }
            ViewBag.EmployeeCode = new SelectList(db.EmployeeGIs, "EmployeeCode", "EmployeeCode", employeeGI.EmployeeCode);
            return View(employeeGI);
        }

        // GET: EmployeeGIs/Edit/5
        public ActionResult MidLevelEdit(int? id)
        {
            EmployeeGI employeeGI = db.EmployeeGIs.Find(id);

            List<SelectListItem> File_Nos = new List<SelectListItem>();
            File_Nos.Add(new SelectListItem { Text = "Select", Value = "0" });
            for (int i = 1; i <= 200; i++)
            {
                //if (i != 9)
                File_Nos.Add(new SelectListItem { Text = i.ToString(), Value = i.ToString() });
                //else
                //    File_Nos.Add(new SelectListItem { Text = i.ToString(), Value = i.ToString(), Selected = true });
            }

            List<SelectListItem> Titles = new List<SelectListItem>();
            //Titles.Add(new SelectListItem { Text = "Select", Value = "0" });
            Titles.Add(new SelectListItem { Text = "Mr", Value = "0" });
            Titles.Add(new SelectListItem { Text = "Mrs", Value = "1" });
            Titles.Add(new SelectListItem { Text = "Miss", Value = "2" });
            Titles.Add(new SelectListItem { Text = "Dr", Value = "3" });
            Titles.Add(new SelectListItem { Text = "Prof", Value = "4" });
            ViewBag.Titles = new SelectList(Titles, "Text", "Text");

            List<SelectListItem> Steps = new List<SelectListItem>();
            Steps.Add(new SelectListItem { Text = "Select", Value = "0" });
            for (int i = 1; i <= 15; i++)
            {
                Steps.Add(new SelectListItem { Text = i.ToString(), Value = i.ToString() });
            }
            List<SelectListItem> Grades = new List<SelectListItem>();
            Grades.Add(new SelectListItem { Text = "Select", Value = "0" });
            for (int i = 1; i <= 15; i++)
            {
                Grades.Add(new SelectListItem { Text = i.ToString(), Value = i.ToString() });
            }
            //ViewBag.File_No = new SelectList(File_Nos, "Value", "Text", employeeGI.File_No);

            ViewBag.Steps = new SelectList(Steps, "Value", "Text");
            ViewBag.Grade_Levels = new SelectList(Grades, "Value", "Text");
            //ViewBag.Rank = new SelectList(db.RankMasters, "RankName", "RankName", employeeGI.Rank);
            //ViewBag.LGA = new SelectList(db.CityMasters, "City", "City", employeeGI.LGA);
            //ViewBag.StateOfOrigin = new SelectList(db.StateMasters, "State", "State", employeeGI.StateOfOrigin);
            //ViewBag.cadre = new SelectList(db.CadreMasters, "CadreName", "CadreName");
            //ViewBag.Programmes = new SelectList(db.ProgrammeMasters, "ProgrammeName", "ProgrammeName");
            //ViewBag.Unit_Research = new SelectList(db.UnitResearchMasters, "UnitResearchName", "UnitResearchName");
            //ViewBag.Unit_Services = new SelectList(db.UnitServicesMasters, "UnitServicesName", "UnitServicesName");
            //ViewBag.StationOfDeployment = new SelectList(db.StationMasters, "StationName", "StationName");
            //ViewBag.Section = new SelectList(db.SectionMasters, "SectionName", "SectionName");
            ViewBag.File_Noa = new SelectList(File_Nos, "Value", "Text");

            ///////
            ViewBag.Ranks = new SelectList(db.RankMasters, "RankName", "RankName");
            ViewBag.LGAs = new SelectList(db.CityMasters, "City", "City");
            ViewBag.StateOfOrigins = new SelectList(db.StateMasters, "State", "State");
            ViewBag.cadres = new SelectList(db.CadreMasters, "CadreName", "CadreName");
            ViewBag.Programmess = new SelectList(db.ProgrammeMasters, "ProgrammeName", "ProgrammeName");
            ViewBag.Unit_Researchs = new SelectList(db.UnitResearchMasters, "UnitResearchName", "UnitResearchName");
            ViewBag.Unit_Servicess = new SelectList(db.UnitServicesMasters, "UnitServicesName", "UnitServicesName");
            ViewBag.StationOfDeployments = new SelectList(db.StationMasters, "StationName", "StationName");
            ViewBag.Sections = new SelectList(db.SectionMasters, "SectionName", "SectionName");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (employeeGI == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeCode = new SelectList(db.EmployeeGIs, "EmployeeCode", "EmployeeCode", employeeGI.EmployeeCode);

            return View(employeeGI);
        }

        // POST: EmployeeGIs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MidLevelEdit([Bind(Include = "EmployeeGIId,EmployeeCode,Rank,File_No,Grade_Level,Step,Cadre,Title,First_Name,Middle_Name,Surname,Sex,DateOfBirth,PlaceOfBirth,Marital_Status,Maiden_Name,Spouse_Name,StateOfOrigin,LGA,Home_Town,Religion,ContactHomeAddress,FirstAppointmentDate,FirstAppointmentLocation,ConfirmationDate,DateOfRetirement,LastPromotionDate,Programmes,Unit_Services,Unit_Research,Section,LeaveDays,Leave_fromDate,Leave_ToDate,EmployeePhoto,EmployeePhotoImage,StationOfDeployment,IsDeleted,CreatedDate")] EmployeeGI employeeGI)
        {
            try
            {
                if (employeeGI.EmployeePhotoImage != null && employeeGI.EmployeePhotoImage.ContentLength > 0)
                {
                    var uploadDir = "~/uploads";
                    var imagePath = Path.Combine(Server.MapPath(uploadDir), employeeGI.EmployeePhotoImage.FileName.ToString());
                    var imageUrl = Path.Combine(uploadDir, employeeGI.EmployeePhotoImage.FileName.ToString());
                    employeeGI.EmployeePhoto = employeeGI.EmployeePhotoImage.FileName.ToString();
                    employeeGI.EmployeePhotoImage.SaveAs(imagePath);

                }
                if (employeeGI.Marital_Status != "Married")
                    employeeGI.Spouse_Name = "N/A";
                employeeGI.Leave_fromDate = DateTime.Now.Date;
                employeeGI.Leave_ToDate = DateTime.Now.Date;
                TimeSpan daydiff = Convert.ToDateTime(employeeGI.Leave_ToDate) - Convert.ToDateTime(employeeGI.Leave_fromDate);
                employeeGI.LeaveDays = daydiff.Days;
                employeeGI.CreatedDate = DateTime.Now.Date;
                employeeGI.IsDeleted = false;
                db.Entry(employeeGI).State = EntityState.Modified;
                db.SaveChanges();
                TempData["successmsg"] = "Record is Successfully Updated";
                TempData["msg"] = "";
                int empcode = Convert.ToInt32(@Session["employeecode"]);
                return RedirectToAction("MidLevelIndex");
            }
            catch (Exception ext)
            {
                TempData["successmsg"] = "";
                TempData["msg"] = "Record is not Updated,Try again";
            }
            return RedirectToAction("Index");
        }
        #endregion
        ///
        // GET: EmployeeGIs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeGI employeeGI = db.EmployeeGIs.Find(id);
            if (employeeGI == null)
            {
                return HttpNotFound();
            }
            return View(employeeGI);
        }

        // POST: EmployeeGIs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {

                EmployeeGI employeeGI = db.EmployeeGIs.Find(id);
                db.EmployeeGIs.Remove(employeeGI);
                db.SaveChanges();

                db.SaveChanges();
                TempData["successmsg"] = "Record is Successfully Deleted";
                TempData["msg"] = "";
            }
            catch (Exception ext)
            {
                TempData["successmsg"] = "";
                TempData["msg"] = "Please delete Other Information records of this Employee, before General Information deletion";
            }
            return RedirectToAction("Index");
        }


        public ActionResult FullDetails(int? id)
        {
            EmployeeAll empall = new EmployeeAll();

            empall.employeegi = (from a in db.EmployeeGIs where a.EmployeeCode == id select a).ToList();
            empall.employeeai = (from a in db.EmployeeAIs where a.EmployeeCode == id select a).ToList();
            empall.employeemi = (from a in db.EmployeeMIs where a.EmployeeCode == id select a).ToList();
            empall.employeesi = (from a in db.EmployeeSIs where a.EmployeeCode == id select a).ToList();
            empall.employeepi = (from a in db.EmployeePIs where a.EmployeeCode == id select a).ToList();
            empall.employeeassociation = (from a in db.EmpAIAssociations where a.EmployeeCode == id select a).ToList();
            empall.employeeconference = (from a in db.EmpAIConferences where a.EmployeeCode == id select a).ToList();

            empall.employeeleaveledger = (from a in db.LeaveLedgers where a.EmployeeCode == id select a).ToList();

            return View(empall);

        }


        //public ActionResult FullDetails(int? id)
        //{

        //    var empDetails = from a in db.EmployeeGIs
        //                     join b in db.EmployeeAIs on a.EmployeeCode equals b.EmployeeCode
        //                     join c in db.EmployeePIs on a.EmployeeCode equals c.EmployeeCode
        //                     join d in db.EmployeeMIs on a.EmployeeCode equals d.EmployeeCode
        //                     join e in db.EmployeeSIs on a.EmployeeCode equals e.EmployeeCode
        //                     from f in db.EmpAIAssociations
        //                     from g in db.EmpAIConferences
        //                     where (a.EmployeeCode == id && (b.EmployeeCode == id || b.EmployeeCode.ToString() == null) && (c.EmployeeCode == id || c.EmployeeCode.ToString() == null) && (d.EmployeeCode == id || d.EmployeeCode.ToString() == null) && (e.EmployeeCode == id || e.EmployeeCode.ToString() == null) && (f.EmployeeCode == id || f.EmployeeCode.ToString() == null) || (g.EmployeeCode == id || g.EmployeeCode.ToString() == null))
        //                     select new EmployeeDetails() { employeeGI = a, employeeAI = b, employeePI = c, employeeMI = d, employeeSI = e, empassociation = f, empconference = g };

        //    return View(empDetails);
        //}

        //public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        //{

        //    ViewBag.CurrentSort = sortOrder;
        //    ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
        //    ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

        //    if (searchString != null)
        //    {
        //        page = 1;
        //    }
        //    else
        //    {
        //        searchString = currentFilter;
        //    }

        //    ViewBag.CurrentFilter = searchString;

        //    var leadss = from s in db.EmployeeGIs
        //                 select s;
        //    //if (!String.IsNullOrEmpty(searchString))
        //    //{
        //    //    leadss = leadss.Where(s => s.First_Name.Contains(searchString));

        //    //}
        //    //switch (sortOrder)
        //    //{
        //    //    case "name_desc":
        //    //        leadss = leadss.OrderByDescending(s => s.First_Name);
        //    //        break;
        //    //    case "Date":
        //    //        leadss = leadss.OrderBy(s => s.FirstAppointmentDate);
        //    //        break;
        //    //    case "date_desc":
        //    //        leadss = leadss.OrderByDescending(s => s.FirstAppointmentDate);
        //    //        break;
        //    //    default:  // Name ascending 
        //    //        leadss = leadss.OrderBy(s => s.EmployeeCode);
        //    //        break;
        //    //}

        //    int pageSize = 7;
        //    int pageNumber = (page ?? 1);
        //    return View(leadss.ToPagedList(pageNumber, pageSize));
        //}

        [HttpPost]
        public JsonResult AutoEmployeeCode(string Prefix)
        {
            //Note : you can bind same list from database  
            ViewBag.emp = new SelectList(db.EmployeeGIs, "EmployeeCode", "EmployeeCode").ToList();
            List<EmployeeGI> objlist = ViewBag.emp;
            //Searching records from list using LINQ query  
            var CityList = (from N in objlist
                            where N.EmployeeCode.ToString().StartsWith(Prefix)
                            select new { N.EmployeeCode });
            return Json(CityList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AnnualLeave(AnnualLeave obj)
        {
            try
            {
                db.AnnualLeaves.Add(obj);

                db.SaveChanges();
                return Json(obj);
            }
            catch (Exception ext)
            {
                return Json(ext);
            }
        }


        [HttpPost]
        public JsonResult CausalLeave(CasualLeave obj)
        {
            try
            {
                db.CasualLeaves.Add(obj);

                db.SaveChanges();
                return Json(obj);
            }
            catch (Exception ext)
            {
                return Json(ext);
            }
        }

        [HttpPost]
        public JsonResult GraduateAttachmentForm(GraduateAttachmentForm obj)
        {
            try
            {
                db.GraduateAttachmentForms.Add(obj);

                db.SaveChanges();
                return Json(obj);
            }
            catch (Exception ext)
            {
                return Json(ext);
            }
        }


        public ActionResult AnnualLeave()
        {
            ViewBag.EmployeeCode = new SelectList(db.EmployeeGIs, "EmployeeCode", "EmployeeCode");


            return View();
        }
        [HttpGet]
        public JsonResult getEmployeedetail(int EmployeeCode)
        {
            var response = (from a in db.EmployeeGIs
                            from b in db.EmployeePIs
                            from c in db.EmployeeSIs
                            where (a.EmployeeCode == EmployeeCode && b.EmployeeCode == a.EmployeeCode && c.EmployeeCode == a.EmployeeCode)
                            select new
                            {
                                empcode = a.EmployeeCode,
                                SurName = a.Surname,
                                Name = a.First_Name + " " + a.Middle_Name,
                                Programmes = a.Programmes,
                                Unit_Services = a.Unit_Services,
                                Unit_Research = a.Unit_Research,
                                MaritalStatus = a.Marital_Status,
                                PhoneNo = b.MobileNo,
                                FileNo = a.File_No,
                                EmployeeStatus = b.EmployeeStatus,
                                SalaryScale = c.SalaryScale,
                                Rank = a.Rank,
                                Cadre = a.Cadre,
                                BankAccNo = c.AccountNumber




                            });

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Monthly()
        {
            return View();
        }

        [HttpPost]
        public JsonResult MonthlyClearance(NyscMonthlyClearance obj)
        {

            db.NyscMonthlyClearances.Add(obj);

            db.SaveChanges();
            return Json(obj);
        }

        [HttpPost]
        public JsonResult FinalClearance(NyscFinalClearance obj)
        {

            db.NyscFinalClearances.Add(obj);

            db.SaveChanges();
            return Json(obj);
        }
        public ActionResult Final()
        {


            return View();
        }

        public ActionResult Graduateattachement()
        {


            return View();
        }
        [HttpPost]
        public JsonResult Graduateattachement(GraduateAttachmentForm obj)
        {

            db.GraduateAttachmentForms.Add(obj);

            db.SaveChanges();
            return Json(obj);
        }

        [HttpPost]
        public JsonResult CasualLeave(CasualLeave obj)
        {

            db.CasualLeaves.Add(obj);

            db.SaveChanges();
            return Json(obj);
        }

        public ActionResult Casualleave()
        {


            return View();
        }








        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
