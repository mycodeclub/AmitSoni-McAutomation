using IARTAutomationApp.Models;
using IARTAutomationApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace IARTAutomationApp.Controllers
{

    public class VendorController : Controller
    {
        // GET: Vendor
        private IARTDBNEWEntities db = new IARTDBNEWEntities();
        public ActionResult Create()
        {
            ViewBag.Status = new SelectList(db.StatusMasters, "RecordId", "StatusName");
            ViewBag.State = new SelectList(GetState(), "Value", "Text");
            ViewBag.Country = new SelectList(GetCountry(), "Value", "Text");
            List<SelectListItem> emptyList = new List<SelectListItem>();
            emptyList.Insert(0, new SelectListItem { Text = "Select City", Value = "" });
            ViewBag.Empty = new SelectList(emptyList, "Value", "Text");
            return View();
        }

        private static List<SelectListItem> GetCountry()
        {
            IARTDBNEWEntities db = new IARTDBNEWEntities();
            List<SelectListItem> storeStatus = (from p in db.COUNTRYLISTs.AsEnumerable()
                                                select new SelectListItem
                                                {
                                                    Text = p.NICENAME,
                                                    Value = p.ID.ToString()
                                                }).ToList();
            storeStatus.Insert(0, new SelectListItem { Text = "Select Country", Value = "" });
            return storeStatus;
        }
        private static List<SelectListItem> GetCity(int stateId)
        {
            IARTDBNEWEntities db = new IARTDBNEWEntities();
            List<SelectListItem> storeStatus = (from p in db.CityMasters.AsEnumerable()
                                                where p.StateId == stateId
                                                select new SelectListItem
                                                {
                                                    Text = p.City,
                                                    Value = p.Id.ToString()
                                                }).ToList();
            storeStatus.Insert(0, new SelectListItem { Text = "Select City", Value = "" });
            return storeStatus;
        }
        private static List<SelectListItem> GetState()
        {
            IARTDBNEWEntities db = new IARTDBNEWEntities();
            List<SelectListItem> storeStatus = (from p in db.StateMasters.AsEnumerable()
                                                select new SelectListItem
                                                {
                                                    Text = p.State,
                                                    Value = p.Id.ToString()
                                                }).ToList();
            storeStatus.Insert(0, new SelectListItem { Text = "Select State", Value = "" });
            return storeStatus;
        }

        public ActionResult GetCityList(int stateId)
        {
            List<SelectListItem> city = GetCity(stateId);

            return Json(new SelectList(city, "Value", "Text", JsonRequestBehavior.AllowGet));
        }


        [HttpPost]
        public ActionResult Create([Bind(Include = "CustomerId,VendorName,VendorOrg,VendorMob,VendorAMob,VendorEmail,VendorRepDesc,VendorAdd1,VendorAdd2,VendorCity,VendorState,VendorCountry,VendorZipCode,VendorTaxDet,VendorStatus,VendorDesc")] VendorMaster vendor)
        {
            VendorMaster v = new VendorMaster();
            if (ModelState.IsValid)
            {
                v.EmployeeID = Convert.ToInt32(@Session["employeecode"]);
                v.VendorName = vendor.VendorName;
                v.VendorOrg = vendor.VendorOrg;
                v.VendorMob = vendor.VendorMob;
                v.VendorAMob = vendor.VendorAMob;
                v.VendorEmail = vendor.VendorEmail;
                v.VendorRepDesc = vendor.VendorRepDesc;
                v.VendorAdd1 = vendor.VendorAdd1;
                v.VendorAdd2 = vendor.VendorAdd2;
                v.VendorCity = vendor.VendorCity;
                v.VendorState = vendor.VendorState;
                v.VendorCountry = vendor.VendorCountry;
                v.VendorZipCode = vendor.VendorZipCode;
                v.VendorTaxDet = vendor.VendorTaxDet;
                v.VendorStatus = vendor.VendorStatus;
                v.VendorDesc = vendor.VendorDesc;
                v.CreatedDate = DateTime.Now;
                db.VendorMasters.Add(v);
                db.SaveChanges();
            }
            return RedirectToAction("Create");
        }

        public ActionResult ViewAll()
        {
            var user = (IARTAutomationApp.Models.UserMaster)Session["User"];
            ViewBag.VendorActiveCount = (from a in db.VendorMasters where a.CustomerId == user.CustomerId && a.VendorStatus == 1 select a).ToList().Count();
            ViewBag.VendorClosedCount = (from a in db.VendorMasters where a.CustomerId == user.CustomerId && a.VendorStatus == 2 select a).ToList().Count();
            ViewBag.TotalVendorCount = (from a in db.VendorMasters where a.CustomerId == user.CustomerId select a).ToList().Count();

            var vendorList = from a in db.VendorMasters
                             where a.CustomerId == user.CustomerId
                             join b in db.COUNTRYLISTs on a.VendorCountry equals b.ID
                             join c in db.StatusMasters on a.VendorStatus equals c.RecordId
                             join d in db.UserMasters on a.EmployeeID equals d.EmployeeCode
                             join e in db.StateMasters on a.VendorState equals e.Id
                             join f in db.CityMasters on a.VendorState equals f.StateId
                             where a.VendorCity == f.Id
                             select new VendorDetails() { vendor = a, country = b, status = c, empName = d.UserName, city = f.City, state = e.State };
            return View(vendorList);
        }

        public ActionResult Edit(int? id)
        {
            ViewBag.Status = new SelectList(db.StatusMasters, "RecordId", "StatusName");
            ViewBag.State = new SelectList(GetState(), "Value", "Text");
            ViewBag.Country = new SelectList(GetCountry(), "Value", "Text");
            List<SelectListItem> emptyList = new List<SelectListItem>();
            emptyList.Insert(0, new SelectListItem { Text = "Select City", Value = "" });
            ViewBag.Empty = new SelectList(emptyList, "Value", "Text");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VendorMaster ven = db.VendorMasters.Find(id);
            if (ven == null)
            {
                return HttpNotFound();
            }
            return View(ven);

        }

        [HttpPost]
        public ActionResult Edit([Bind(Include = "CustomerId,RecordId,VendorName,VendorOrg,VendorMob,VendorAMob,VendorEmail,VendorRepDesc,VendorAdd1,VendorAdd2,VendorCity,VendorState,VendorCountry,VendorZipCode,VendorTaxDet,VendorStatus,VendorDesc")] VendorMaster vendor)
        {

            VendorMaster v = (from c in db.VendorMasters
                              where c.RecordId == vendor.RecordId
                              select c).FirstOrDefault();
            if (ModelState.IsValid)
            {
                v.EmployeeID = Convert.ToInt32(@Session["employeecode"]);
                v.VendorName = vendor.VendorName;
                v.VendorOrg = vendor.VendorOrg;
                v.VendorMob = vendor.VendorMob;
                v.VendorAMob = vendor.VendorAMob;
                v.VendorEmail = vendor.VendorEmail;
                v.VendorRepDesc = vendor.VendorRepDesc;
                v.VendorAdd1 = vendor.VendorAdd1;
                v.VendorAdd2 = vendor.VendorAdd2;
                v.VendorCity = vendor.VendorCity;
                v.VendorState = vendor.VendorState;
                v.VendorCountry = vendor.VendorCountry;
                v.VendorZipCode = vendor.VendorZipCode;
                v.VendorTaxDet = vendor.VendorTaxDet;
                v.VendorStatus = vendor.VendorStatus;
                v.VendorDesc = vendor.VendorDesc;
                v.CreatedDate = DateTime.Now;
                db.SaveChanges();
            }
            return RedirectToAction("ViewAll");
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VendorMaster ven = db.VendorMasters.Find(id);
            var vendorList = (from a in db.VendorMasters
                              join b in db.COUNTRYLISTs on a.VendorCountry equals b.ID
                              join c in db.StatusMasters on a.VendorStatus equals c.RecordId
                              join d in db.UserMasters on a.EmployeeID equals d.EmployeeCode
                              join e in db.StateMasters on a.VendorState equals e.Id
                              join f in db.CityMasters on a.VendorState equals f.StateId
                              where a.VendorCity == f.Id
                              select new VendorDetails() { vendor = a, country = b, status = c, empName = d.UserName, city = f.City, state = e.State }).Single();

            if (ven == null)
            {
                return HttpNotFound();
            }
            return View(vendorList);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VendorMaster ven = db.VendorMasters.Find(id);
            db.VendorMasters.Remove(ven);
            db.SaveChanges();
            return RedirectToAction("ViewAll");
        }

    }

}
