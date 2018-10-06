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

namespace IARTAutomationApp.Controllers
{

    public class UserMastersController : Controller
    {
        private IARTDBNEWEntities db = new IARTDBNEWEntities();

        public ActionResult Edit_ChangePwd(int id)
        {
            List<RoleMaster> Rolelist = new List<RoleMaster>();
            Rolelist = (from a in db.RoleMasters select a).ToList();
            //ViewBag.CountryList = CountryList;
            ViewBag.RoleIds = new SelectList(Rolelist, "RoleId", "RoleName");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserMaster userMaster = (from a in db.UserMasters where a.EmployeeCode == id select a).FirstOrDefault();
            if (userMaster == null)
            {
                return HttpNotFound();
            }
            return View(userMaster);
        }


        public ActionResult MyProfile(int? id)
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

        public ActionResult UserChangePassword(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserMaster userMaster = (from a in db.UserMasters where a.EmployeeCode == id select a).FirstOrDefault();
            if (userMaster == null)
            {
                return HttpNotFound();
            }
            return View(userMaster);
        }


        public ActionResult MidLevelMyProfile(int? id)
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

        public ActionResult SuperLevelMyProfile(int? id)
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


        [HttpGet]
        public JsonResult getEmployeedetail(int EmployeeCode)
        {
            var response = (from a in db.EmployeeGIs
                            from b in db.EmployeePIs
                            from c in db.UserMasters

                            where a.EmployeeCode == EmployeeCode && b.EmployeeCode == a.EmployeeCode || c.EmployeeCode == EmployeeCode
                            select new
                            {
                                empcode = a.EmployeeCode,

                                Name = a.Surname + "" + a.First_Name + " " + a.Middle_Name,

                                EmailId = b.EmpEmailId,
                                OrgName = "INSTITUTE OF AGRICULTURAL RESEARCH AND TRAINING",

                                Password = c.Password




                            });

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        // GET: UserMasters
        public ActionResult Index()
        {
            var user = (IARTAutomationApp.Models.UserMaster)Session["User"];

            var NoofUser = (from a in db.UserMasters where a.CustomerId == user.CustomerId select a).ToList().Count();
            ViewBag.NoofUser = NoofUser;
            var employeeRoles = db.UserMasters.Where(e => e.CustomerId == user.CustomerId);

            return View(employeeRoles);
        }

        // GET: UserMasters/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserMaster userMaster = db.UserMasters.Find(id);
            if (userMaster == null)
            {
                return HttpNotFound();
            }
            return View(userMaster);
        }

        // GET: UserMasters/Create
        public ActionResult Create()
        {
            List<RoleMaster> Rolelist = new List<RoleMaster>();
            Rolelist = (from a in db.RoleMasters select a).ToList();
            //ViewBag.CountryList = CountryList;
            ViewBag.RoleId = new SelectList(Rolelist, "RoleId", "RoleName");
            return View();
        }

        // POST: UserMasters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CustomerId,EmployeeCode,UserId,EmailId,UserName,Password,RoleId,UserKeyId,OrganizationName,IsActive,LastLoginDate,IsDeleted,CreatedDate")] UserMaster userMaster)
        {
            if (ModelState.IsValid)
            {
                db.UserMasters.Add(userMaster);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(userMaster);
        }

        // GET: UserMasters/Edit/5
        public ActionResult Edit(int? id)
        {
            List<RoleMaster> Rolelist = new List<RoleMaster>();
            Rolelist = (from a in db.RoleMasters select a).ToList();
            //ViewBag.CountryList = CountryList;
            ViewBag.RoleIds = new SelectList(Rolelist, "RoleId", "RoleName");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserMaster userMaster = db.UserMasters.Find(id);
            if (userMaster == null)
            {
                return HttpNotFound();
            }
            return View(userMaster);
        }

        // POST: UserMasters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustomerId,EmployeeCode,EmailId,UserId,UserName,Password,RoleId,UserKeyId,OrganizationName")] UserMaster userMaster)
        //,IsActive,LastLoginDate,IsDeleted,CreatedDate
        {
            if (ModelState.IsValid)
            {
                //var userId = (from a in db.UserMasters where a.EmailId == userMaster.EmailId select a.UserId ).FirstOrDefault();
                //userMaster.UserId =Convert.ToInt32( userId);
                db.Entry(userMaster).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userMaster);
        }

        // GET: UserMasters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserMaster userMaster = db.UserMasters.Find(id);
            if (userMaster == null)
            {
                return HttpNotFound();
            }
            return View(userMaster);
        }

        // POST: UserMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserMaster userMaster = db.UserMasters.Find(id);
            db.UserMasters.Remove(userMaster);
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
    }
}
