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
    public class AnnualLeavesController : Controller
    {
        private IARTDBNEWEntities db = new IARTDBNEWEntities();

        // GET: AnnualLeaves
        public ActionResult Index()
        {
            var annualLeaves = db.AnnualLeaves.Include(a => a.EmployeeGI).Include(a => a.EmployeeGI1);
            return View(annualLeaves.ToList());
        }

        // GET: AnnualLeaves/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AnnualLeave annualLeave = db.AnnualLeaves.Find(id);
            if (annualLeave == null)
            {
                return HttpNotFound();
            }
            return View(annualLeave);
        }

        // GET: AnnualLeaves/Create
        public ActionResult Create()
        {
            ViewBag.EmployeeCode = new SelectList(db.EmployeeGIs, "EmployeeCode", "Rank");
            ViewBag.EmployeeCode = new SelectList(db.EmployeeGIs, "EmployeeCode", "Rank");
            return View();
        }

        // POST: AnnualLeaves/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,EmployeeCode,Surname,OtherNames,Faculty,Department,Maritalstatus,Nationality,PhoneNo,Fileno,PresentStatus,Salaryperannum,Proposedannualleave,LeavefromDate,LeavetoDate,Totalworkingday,IsLeave,IsLeavefromDate,IsLeavetoDate,OutstandingLeaveDays,IsPublicService,IsHOD,ActOfficer,IApprove,IsDeleted,CreatedDate")] AnnualLeave annualLeave)
        {
            if (ModelState.IsValid)
            {
                db.AnnualLeaves.Add(annualLeave);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EmployeeCode = new SelectList(db.EmployeeGIs, "EmployeeCode", "Rank", annualLeave.EmployeeCode);
            ViewBag.EmployeeCode = new SelectList(db.EmployeeGIs, "EmployeeCode", "Rank", annualLeave.EmployeeCode);
            return View(annualLeave);
        }

        // GET: AnnualLeaves/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AnnualLeave annualLeave = db.AnnualLeaves.Find(id);
            if (annualLeave == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeCode = new SelectList(db.EmployeeGIs, "EmployeeCode", "Rank", annualLeave.EmployeeCode);
            ViewBag.EmployeeCode = new SelectList(db.EmployeeGIs, "EmployeeCode", "Rank", annualLeave.EmployeeCode);
            return View(annualLeave);
        }

        // POST: AnnualLeaves/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,EmployeeCode,Surname,OtherNames,Faculty,Department,Maritalstatus,Nationality,PhoneNo,Fileno,PresentStatus,Salaryperannum,Proposedannualleave,LeavefromDate,LeavetoDate,Totalworkingday,IsLeave,IsLeavefromDate,IsLeavetoDate,OutstandingLeaveDays,IsPublicService,IsHOD,ActOfficer,IApprove,IsDeleted,CreatedDate")] AnnualLeave annualLeave)
        {
            if (ModelState.IsValid)
            {
                db.Entry(annualLeave).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeCode = new SelectList(db.EmployeeGIs, "EmployeeCode", "Rank", annualLeave.EmployeeCode);
            ViewBag.EmployeeCode = new SelectList(db.EmployeeGIs, "EmployeeCode", "Rank", annualLeave.EmployeeCode);
            return View(annualLeave);
        }

        // GET: AnnualLeaves/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AnnualLeave annualLeave = db.AnnualLeaves.Find(id);
            if (annualLeave == null)
            {
                return HttpNotFound();
            }
            return View(annualLeave);
        }

        // POST: AnnualLeaves/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AnnualLeave annualLeave = db.AnnualLeaves.Find(id);
            db.AnnualLeaves.Remove(annualLeave);
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
