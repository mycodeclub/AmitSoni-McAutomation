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

    public class LeaveMastersController : Controller
    {
        private IARTDBNEWEntities db = new IARTDBNEWEntities();

        // GET: LeaveMasters
        public ActionResult Index()
        {
            var user = (IARTAutomationApp.Models.UserMaster)Session["User"];
            return View(db.LeaveMasters.Where(e => e.CustomerId == user.CustomerId).ToList());
        }

        // GET: LeaveMasters/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LeaveMaster leaveMaster = db.LeaveMasters.Find(id);
            if (leaveMaster == null)
            {
                return HttpNotFound();
            }
            return View(leaveMaster);
        }

        // GET: LeaveMasters/Create
        public ActionResult Create()
        {
            List<LeaveTypeMaster> LeaveTypeList = new List<LeaveTypeMaster>();
            LeaveTypeList = (from a in db.LeaveTypeMasters select a).ToList();
            //ViewBag.CountryList = CountryList;
            ViewBag.LeaveTypeIdS = new SelectList(LeaveTypeList, "LeaveTypeName", "LeaveTypeName");

            return View();
        }

        // POST: LeaveMasters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CustomerId,LeaveId,LeaveTypeId,LeaveCount,CalenderYear,IsActive,IsDelete,IsCreated")] LeaveMaster leaveMaster)
        {
            if (ModelState.IsValid)
            {
                db.LeaveMasters.Add(leaveMaster);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(leaveMaster);
        }

        // GET: LeaveMasters/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LeaveMaster leaveMaster = db.LeaveMasters.Find(id);
            if (leaveMaster == null)
            {
                return HttpNotFound();
            }
            return View(leaveMaster);
        }

        // POST: LeaveMasters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustomerId,LeaveId,LeaveTypeId,LeaveCount,CalenderYear,IsActive,IsDelete,IsCreated")] LeaveMaster leaveMaster)
        {
            if (ModelState.IsValid)
            {
                db.Entry(leaveMaster).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(leaveMaster);
        }

        // GET: LeaveMasters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LeaveMaster leaveMaster = db.LeaveMasters.Find(id);
            if (leaveMaster == null)
            {
                return HttpNotFound();
            }
            return View(leaveMaster);
        }

        // POST: LeaveMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LeaveMaster leaveMaster = db.LeaveMasters.Find(id);
            db.LeaveMasters.Remove(leaveMaster);
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
