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

    public class UomController : Controller
    {
        private IARTDBNEWEntities db = new IARTDBNEWEntities();
        public ActionResult Create()
        {
            ViewBag.Status = new SelectList(db.StatusMasters, "RecordId", "StatusName");
            return View();
        }

        [HttpPost]
        public ActionResult Create([Bind(Include = "CustomerId,UOMName, UOMCode, UOMDesc, UOMStatus")] UomMaster uomMaster)
        {
            UomMaster uom = new UomMaster();
            {
                ModelState.Remove("EmployeeID");
                if (ModelState.IsValid)
                    uom.EmployeeID = Convert.ToInt32(@Session["employeecode"]);
                uom.UOMName = uomMaster.UOMName;
                uom.UOMDesc = uomMaster.UOMDesc;
                uom.UOMStatus = uomMaster.UOMStatus;
                uom.UOMCode = uomMaster.UOMCode;
                uom.CreatedDate = DateTime.Now;
                db.UomMasters.Add(uom);
                db.SaveChanges();


            }
            return RedirectToAction("Create");
        }


        public ActionResult ViewAll()
        {
            var user = (IARTAutomationApp.Models.UserMaster)Session["User"];

            ViewBag.TotalUomCount = (from a in db.UomMasters where a.CustomerId == user.CustomerId select a).ToList().Count();
            ViewBag.UomActiveCount = (from a in db.UomMasters where a.UOMStatus == 1 && a.CustomerId == user.CustomerId select a).ToList().Count();
            ViewBag.UomClosedCount = (from a in db.UomMasters where a.UOMStatus == 2 && a.CustomerId == user.CustomerId select a).ToList().Count();
            var uomList = from a in db.UomMasters
                          where a.CustomerId == user.CustomerId
                          join d in db.UserMasters on a.EmployeeID equals d.EmployeeCode
                          join c in db.StatusMasters on a.UOMStatus equals c.RecordId
                          select new UomDetails() { uom = a, empName = d.UserName, status = c.StatusName };
            return View(uomList);

        }

        public ActionResult Edit(int? id)
        {
            ViewBag.Status = new SelectList(db.StatusMasters, "RecordId", "StatusName");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UomMaster uom = db.UomMasters.Find(id);
            if (uom == null)
            {
                return HttpNotFound();
            }
            return View(uom);
        }

        [HttpPost]
        public ActionResult Edit([Bind(Include = "CustomerId,RecordId,UOMName, UOMCode, UOMDesc,UOMStatus")] UomMaster uomMaster)
        {
            UomMaster uom = (from c in db.UomMasters
                             where c.RecordId == uomMaster.RecordId
                             select c).FirstOrDefault();
            uom.EmployeeID = Convert.ToInt32(@Session["employeecode"]);
            uom.UOMName = uomMaster.UOMName;
            uom.UOMDesc = uomMaster.UOMDesc;
            uom.UOMStatus = uomMaster.UOMStatus;
            uom.UOMCode = uomMaster.UOMCode;
            uom.CreatedDate = DateTime.Now;
            db.SaveChanges();
            return RedirectToAction("ViewAll");
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UomMaster uom = db.UomMasters.Find(id);
            if (uom == null)
            {
                return HttpNotFound();
            }
            return View(uom);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UomMaster uom = db.UomMasters.Find(id);
            db.UomMasters.Remove(uom);
            db.SaveChanges();
            return RedirectToAction("ViewAll");
        }
    }
}