using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ExcelDataReader;
using IARTAutomationApp.Models;

namespace IARTAutomationApp.Controllers
{
    [Authorize(Roles = "SuperAdmin")]

    public class TenantController : Controller
    {
        private string validImageFormets = @"bmp, jpg, jpeg, gif, png";

        private IARTDBNEWEntities db = new IARTDBNEWEntities();

        // GET: Tenant
        public ActionResult Index()
        {
            ViewBag.NoOfTenants = db.CustomerMasters.Count();
            return View(db.CustomerMasters.Include("UserMaster").OrderByDescending(c => c.CustomerId).ToList());
        }

        // GET: Tenant/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerMaster customerMaster = db.CustomerMasters.Find(id);
            if (customerMaster == null)
            {
                return HttpNotFound();
            }
            return View(customerMaster);
        }

        // GET: CustomerMasters/Create
        public ActionResult Create()
        {
            ViewBag.LGAs = new SelectList(db.CityMasters.Where(c => c.StateId == 1), "City", "City");
            ViewBag.StateOfOrigins = new SelectList(db.StateMasters, "State", "State");
            var emp = new EmployeeGI() { CustomerMaster = new CustomerMaster() };
            return View();
        }

        // POST: CustomerMasters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EmployeeGI tenent)
        {
            if (ModelState.IsValid)
            {
                if (AddNewTenant(tenent))
                {
                    TempData["NewTenent"] = tenent.CustomerMaster;
                    return RedirectToAction("Index", "Tenant");
                }
            }
            ViewBag.LGAs = new SelectList(db.CityMasters.Where(c => c.StateId == 1), "City", "City");
            ViewBag.StateOfOrigins = new SelectList(db.StateMasters, "State", "State");
            return View(tenent);
        }
        public ActionResult Edit(int? id)
        { // id = EmployeeGIId
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var tenent = db.EmployeeGIs.Where(emp => emp.EmployeeGIId == id.Value).FirstOrDefault();
            if (tenent == null) return HttpNotFound();
            ViewBag.LGAs = new SelectList(db.CityMasters.Where(c => c.StateId == 1), "City", "City");
            ViewBag.StateOfOrigins = new SelectList(db.StateMasters, "State", "State");
            return View(tenent);
        }
        // POST: CustomerMasters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EmployeeGI tenent)
        {
            if (ModelState.IsValid)
            {
                var tenentUpdate = db.EmployeeGIs.Find(tenent.EmployeeCode);
                tenentUpdate.First_Name = tenent.First_Name;
                tenentUpdate.Surname = tenent.Surname;
                tenentUpdate.Sex = tenent.Sex;
                tenentUpdate.DateOfBirth = tenent.DateOfBirth;
                tenentUpdate.Maiden_Name = tenent.Maiden_Name;
                tenentUpdate.Middle_Name = tenent.Middle_Name;
                tenentUpdate.Title = tenent.Title;
                tenentUpdate.StateOfOrigin = tenent.StateOfOrigin;
                tenentUpdate.LGA = tenent.LGA;
                tenentUpdate.Religion = tenent.Religion;
                tenentUpdate.DateOfRetirement = tenent.DateOfRetirement;
                tenentUpdate.EmployeeCode = tenent.EmployeeCode;
                tenentUpdate.Unit_Research = tenent.Unit_Research;
                tenentUpdate.Section = tenent.Section;
                tenentUpdate.StationOfDeployment = tenent.StationOfDeployment;
                tenentUpdate.File_No = tenent.File_No;
                tenentUpdate.Grade_Level = tenent.Grade_Level;
                tenentUpdate.Step = tenent.Step;
                tenentUpdate.Cadre = tenent.Cadre;
                tenentUpdate.Marital_Status = tenent.Marital_Status;
                tenentUpdate.PlaceOfBirth = tenent.PlaceOfBirth;
                tenentUpdate.Home_Town = tenent.Home_Town;
                tenentUpdate.ContactHomeAddress = tenent.ContactHomeAddress;
                tenentUpdate.FirstAppointmentDate = tenent.FirstAppointmentDate;
                tenentUpdate.FirstAppointmentLocation = tenent.FirstAppointmentLocation;
                tenentUpdate.ConfirmationDate = tenent.ConfirmationDate;
                tenentUpdate.LastPromotionDate = tenent.LastPromotionDate;
                tenentUpdate.Rank = tenent.Rank;
                db.Entry(tenentUpdate).State = EntityState.Modified;
                var customerUpdate = db.CustomerMasters.Find(tenentUpdate.CustomerId);
                customerUpdate.ContactPerson = tenent.CustomerMaster.ContactPerson = (tenent.First_Name + " " + tenent.Middle_Name + " " + tenent.Surname).Trim();
                customerUpdate.OrgLogoUrl = tenent.CustomerMaster.OrgLogoUrl;
                customerUpdate.CountryLogoIrl = tenent.CustomerMaster.CountryLogoIrl;
                customerUpdate.OrgName = tenent.CustomerMaster.OrgName;
                customerUpdate.Email = tenent.CustomerMaster.Email;
                customerUpdate.Address = tenent.CustomerMaster.Address;
                customerUpdate.PhoneNumber = tenent.CustomerMaster.PhoneNumber;
                customerUpdate.CountryLogo = tenent.CustomerMaster.CountryLogo;
                customerUpdate.CountryLogoIrl = tenent.CustomerMaster.CountryLogoIrl;
                customerUpdate.OrgLogo = tenent.CustomerMaster.OrgLogo;
                customerUpdate.OrgLogoUrl = tenent.CustomerMaster.OrgLogoUrl;


                SaveImages(customerUpdate);
                db.Entry(customerUpdate).State = EntityState.Modified;
                var user = db.UserMasters.Where(um => um.CustomerId == tenent.CustomerId).FirstOrDefault();
                user.Password = tenent.CustomerMaster.UserMaster.Password;
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tenent);
        }
        // GET: CustomerMasters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerMaster customerMaster = db.CustomerMasters.Find(id);
            if (customerMaster == null)
            {
                return HttpNotFound();
            }
            return View(customerMaster);
        }

        // POST: CustomerMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CustomerMaster customerMaster = db.CustomerMasters.Find(id);
            db.CustomerMasters.Remove(customerMaster);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult GetCitiesByState(string stateName)
        {
            var x = db.StateMasters.Where(s => s.State.Equals(stateName)).FirstOrDefault().Id;
            var citys = (from cm in db.CityMasters where cm.StateId == db.StateMasters.Where(s => s.State.Equals(stateName)).FirstOrDefault().Id select cm).ToList();
            return PartialView(citys);
        }
        private bool AddNewTenant(EmployeeGI tenent)
        {
            var isSaved = false;
            try
            {
                tenent.CustomerMaster.ContactPerson = tenent.CustomerMaster.ContactPerson = (tenent.First_Name + " " + tenent.Middle_Name + " " + tenent.Surname).Trim();
                tenent.DateOfRetirement = DateTime.Now.AddYears(20);
                tenent.EmployeeCode = (db.EmployeeGIs.Any()) ? (db.EmployeeGIs.Max(e => e.EmployeeCode) + 1) : 1000;
                var loginUser = new UserMaster()
                {
                    EmployeeCode = tenent.EmployeeCode,
                    EmailId = "Update Your Mail Id",
                    UserName = tenent.EmployeeCode.ToString(),
                    Password = "Pwd" + tenent.EmployeeCode.ToString(),
                    RoleId = 1,
                    RoleName = "Admin",
                };
                db.UserMasters.Add(loginUser);
                db.SaveChanges();
                tenent.CustomerMaster.LoginUserId = loginUser.UserId;
                tenent.CustomerMaster.EmployeeGIId = tenent.EmployeeGIId;
                db.EmployeeGIs.Add(tenent);
                db.SaveChanges();
                tenent.CustomerMaster.EmployeeGIId = tenent.EmployeeGIId;
                loginUser.CustomerId = tenent.CustomerId;
                var isImagesSaved = SaveImages(tenent.CustomerMaster);
                var isConfigSaved = AddSystemConfig(tenent);
                isSaved = isImagesSaved && isConfigSaved;
                var rank = db.RankMasters.Where(r => r.CustomerId == tenent.CustomerMaster.CustomerId).FirstOrDefault()?.RankName;
                tenent.Rank = !string.IsNullOrEmpty(rank) ? rank : string.Empty;
                db.SaveChanges();
                tenent.CustomerMaster.EmployeeGIId = tenent.EmployeeGIId;
                tenent.CustomerId = tenent.CustomerMaster.CustomerId;
                loginUser.CustomerId = tenent.CustomerMaster.CustomerId;
                db.Entry(loginUser).State = EntityState.Modified;
                db.Entry(tenent.CustomerMaster).State = EntityState.Modified;
                db.Entry(tenent).State = EntityState.Modified;
                tenent.CustomerMaster.UserMaster = loginUser;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                isSaved = false;
            }
            return isSaved;
        }
        private bool AddSystemConfig(EmployeeGI tenent)
        {
            // var path = "E:/MyProjects/AmitSoni-McAutomation/IARTAutomationApp/App_Data/SystemConfig.xlsx"; 
            string filepath = Server.MapPath("~/App_Data/SystemConfig.xlsx");
            FileStream fs = System.IO.File.Open(filepath, FileMode.Open, FileAccess.Read);
            IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(fs);
            DataSet ds = excelReader.AsDataSet();
            fs.Close();
            db.RankMasters.AddRange(AddRank(ds.Tables["Rank"].Rows, tenent.CustomerId.Value));// Not reading from excel as data is not avilable in excel.  
            db.CadreMasters.AddRange(AddCadres(ds.Tables["Cadre"].Rows, tenent.CustomerId.Value));
            db.ProgrammeMasters.AddRange(AddProgrammeMasters(ds.Tables["Programmes"].Rows, tenent.CustomerId.Value));
            db.UnitResearchMasters.AddRange(AddUnitResearchMaster(ds.Tables["Unit - Research"].Rows, tenent.CustomerId.Value));
            db.UnitServicesMasters.AddRange(AddUnitServicesMaster(ds.Tables["Unit - Service"].Rows, tenent.CustomerId.Value));
            db.StationMasters.AddRange(AddStationOfDeployment(ds.Tables["Station of Deployment"].Rows, tenent.CustomerId.Value));
            db.SectionMasters.AddRange(AddSectionMaster(ds.Tables["Section"].Rows, tenent.CustomerId.Value));
            db.BankTypeMasters.AddRange(AddTypeOfBank(ds.Tables["Bank Types"].Rows, tenent.CustomerId.Value));
            db.BankMasters.AddRange(AddBank(ds.Tables["Bank Names"].Rows, tenent.CustomerId.Value));
            db.PFAMasters.AddRange(AddPFAMaster(ds.Tables["PFA"].Rows, tenent.CustomerId.Value));
            return db.SaveChanges() > 0 ? true : false;
        }
        private List<RankMaster> AddRank(DataRowCollection rows, int customerId)
        {
            var ranks = new List<RankMaster>() { };
            for (int i = 1; i < rows.Count; i++)
            {
                ranks.Add(new RankMaster()
                {
                    RankName = rows[i][1].ToString(),
                    RankDescription = rows[i][2].ToString(),
                    CreatedDate = System.DateTime.Now,
                    CustomerId = customerId,
                    IsDeleted = false
                });
            }
            return ranks;
        }
        private List<CadreMaster> AddCadres(DataRowCollection rows, int customerId)
        {
            var cadres = new List<CadreMaster>() { };
            for (int i = 1; i < rows.Count - 1; i++)
            {
                cadres.Add(new CadreMaster()
                {
                    CadreName = rows[i][1].ToString(),
                    CreatedDate = System.DateTime.Now,
                    CustomerId = customerId,
                    IsDeleted = false
                });
            }
            return cadres;
        }
        private List<ProgrammeMaster> AddProgrammeMasters(DataRowCollection rows, int customerId)
        {
            var programmes = new List<ProgrammeMaster>() { };
            for (int i = 1; i < rows.Count - 1; i++)
            {
                programmes.Add(new ProgrammeMaster()
                {
                    ProgrammeName = rows[i][1].ToString(),
                    CreatedDate = System.DateTime.Now,
                    CustomerId = customerId,
                    IsDeleted = false
                });
            }
            return programmes;
        }
        private List<UnitResearchMaster> AddUnitResearchMaster(DataRowCollection rows, int customerId)
        {
            var units = new List<UnitResearchMaster>() { };
            for (int i = 1; i < rows.Count - 1; i++)
            {
                units.Add(new UnitResearchMaster()
                {
                    UnitResearchName = rows[i][1].ToString(),
                    CreatedDate = System.DateTime.Now,
                    CustomerId = customerId,
                    IsDeleted = false
                });
            }
            return units;
        }
        private List<UnitServicesMaster> AddUnitServicesMaster(DataRowCollection rows, int customerId)
        {

            var units = new List<UnitServicesMaster>() { };
            for (int i = 1; i < rows.Count - 1; i++)
            {
                units.Add(new UnitServicesMaster()
                {
                    UnitServicesName = rows[i][1].ToString(),
                    CreatedDate = System.DateTime.Now,
                    CustomerId = customerId,
                    IsDeleted = false
                });
            }
            return units;
        }
        private List<StationMaster> AddStationOfDeployment(DataRowCollection rows, int customerId)
        {

            var units = new List<StationMaster>() { };
            for (int i = 1; i < rows.Count - 1; i++)
            {
                units.Add(new StationMaster()
                {
                    StationName = rows[i][1].ToString(),
                    CreatedDate = System.DateTime.Now,
                    CustomerId = customerId,
                    IsDeleted = false
                });
            }
            return units;
        }
        private List<SectionMaster> AddSectionMaster(DataRowCollection rows, int customerId)
        {
            var units = new List<SectionMaster>() { };
            for (int i = 1; i < rows.Count - 1; i++)
            {
                units.Add(new SectionMaster()
                {
                    SectionName = rows[i][1].ToString(),
                    CreatedDate = System.DateTime.Now,
                    CustomerId = customerId,
                    IsDeleted = false
                });
            }
            return units;
        }
        private List<BankTypeMaster> AddTypeOfBank(DataRowCollection rows, int customerId)
        {
            var units = new List<BankTypeMaster>() { };
            for (int i = 1; i < rows.Count - 1; i++)
            {
                units.Add(new BankTypeMaster()
                {
                    BankTypeName = rows[i][1].ToString(),
                    CreatedDate = System.DateTime.Now,
                    CustomerId = customerId,
                    IsDeleted = false
                });
            }
            return units;
        }
        private List<BankMaster> AddBank(DataRowCollection rows, int customerId)
        {
            var units = new List<BankMaster>() { };
            for (int i = 1; i < rows.Count - 1; i++)
            {
                units.Add(new BankMaster()
                {
                    BankName = rows[i][1].ToString(),
                    CreatedDate = System.DateTime.Now,
                    CustomerId = customerId,
                    IsDeleted = false
                });
            }
            return units;
        }
        private List<PFAMaster> AddPFAMaster(DataRowCollection rows, int customerId)
        {
            var units = new List<PFAMaster>() { };
            for (int i = 1; i < rows.Count - 1; i++)
            {
                units.Add(new PFAMaster()
                {
                    PFAName = rows[i][1].ToString(),
                    CreatedDate = System.DateTime.Now,
                    CustomerId = customerId,
                    IsDeleted = false
                });
            }
            return units;
        }



        private bool SaveImages(CustomerMaster customerMaster)
        {
            var gotOrg = false;
            var gotCountry = false;
            if (string.IsNullOrEmpty(customerMaster.OrgLogoUrl) && customerMaster.OrgLogo == null || customerMaster.OrgLogo?.ContentLength == 0)
            {
                customerMaster.OrgLogoUrl = @"/Uploads/Logos/Default/organizationLogo.jpg";
                gotOrg = true;
            }
            if (string.IsNullOrEmpty(customerMaster.CountryLogoIrl) && customerMaster.CountryLogo == null || customerMaster.CountryLogo?.ContentLength == 0)
            {
                customerMaster.OrgLogoUrl = @"/Uploads/Logos/Default/countryLogo.jpg";
                gotCountry = true;
            }
            if (customerMaster.OrgLogo != null && customerMaster.OrgLogo.ContentLength > 0)
            {
                var orgFileName = "org_" + DateTime.UtcNow.ToString().Replace(" ", string.Empty).Replace(":", string.Empty).Replace("/", string.Empty) + customerMaster.OrgLogo.FileName.Replace(" ", string.Empty);
                var path = @"/Uploads/Logos/" + customerMaster.CustomerId + "/";
                if (!Directory.Exists(Server.MapPath("~" + path)))
                    Directory.CreateDirectory(Server.MapPath("~" + path));
                customerMaster.OrgLogoUrl = path + orgFileName;
                customerMaster.OrgLogo.SaveAs(Server.MapPath("~" + customerMaster.OrgLogoUrl));
                gotOrg = true;
            }
            if (customerMaster.CountryLogo != null && customerMaster.CountryLogo.ContentLength > 0)
            {
                var countryFileName = "cou_" + DateTime.UtcNow.ToString().Replace(" ", string.Empty).Replace(":", string.Empty).Replace("/", string.Empty) + customerMaster.CountryLogo.FileName.Replace(" ", string.Empty);
                var path = @"/Uploads/Logos/" + customerMaster.CustomerId + "/";
                if (!Directory.Exists(Server.MapPath("~" + path)))
                    Directory.CreateDirectory(Server.MapPath("~" + path));
                customerMaster.CountryLogoIrl = path + countryFileName;
                customerMaster.CountryLogo.SaveAs(Server.MapPath("~" + customerMaster.CountryLogoIrl));
                gotCountry = true;
            }
            return gotOrg && gotCountry;
        }
    }
}
