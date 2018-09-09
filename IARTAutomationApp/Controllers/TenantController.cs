using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IARTAutomationApp.Models;

namespace IARTAutomationApp.Controllers
{
    [Authorize(Roles = "SuperAdmin")]

    public class TenantController : Controller
    {

        private IARTDBNEWEntities db = new IARTDBNEWEntities();

        // GET: Tenant
        public ActionResult Index()
        {
            var tenEmpIds = (from u in db.UserMasters where u.RoleId == 1 select u.EmployeeCode).ToList();
            var tenetnt = db.EmployeeGIs.Where(emp => tenEmpIds.Contains(emp.EmployeeCode)).ToList();
            return View(tenetnt);
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
            return View();
        }

        // POST: CustomerMasters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "First_Name,Surname,Sex,DateOfBirth,Maiden_Name,Middle_Name,Title,StateOfOrigin,LGA,Religion,DateOfRetirement,EmployeeCode,Unit_Research,Section,StationOfDeployment,File_No,Grade_Level,Step,Cadre,Marital_Status,PlaceOfBirth,Home_Town,ContactHomeAddress,FirstAppointmentDate,FirstAppointmentLocation,ConfirmationDate,LastPromotionDate,Rank")] EmployeeGI tenent)
        {
            if (ModelState.IsValid)
            {
                tenent.DateOfRetirement = DateTime.Now.AddYears(20);
                tenent.EmployeeCode = (db.EmployeeGIs.Max(e => e.EmployeeGIId) + 1);
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
                db.EmployeeGIs.Add(tenent);
                var x = db.SaveChanges();
            }
            ViewBag.LGAs = new SelectList(db.CityMasters.Where(c => c.StateId == 1), "City", "City");
            ViewBag.StateOfOrigins = new SelectList(db.StateMasters, "State", "State");
            return View(tenent);
        }

        // GET: CustomerMasters/Edit/5
        public ActionResult Edit(int? id)
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

        // POST: CustomerMasters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustomerId,Name,LoginName,Password,Phone,Email,Website")] CustomerMaster customerMaster)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customerMaster).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customerMaster);
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
        private bool AddNewTenant(CustomerMaster tenent)
        {

            return true;
        }
    }
}
