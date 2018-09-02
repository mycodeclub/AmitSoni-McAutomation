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
    public class CasualLeavesController : Controller
    {
        private IARTDBNEWEntities db = new IARTDBNEWEntities();

        // GET: CasualLeaves
        public ActionResult Index()
        {
            var casualLeaves = db.CasualLeaves.Include(c => c.EmployeeGI).Include(c => c.EmployeeGI1);
            return View(casualLeaves.ToList());
        }

        // GET: CasualLeaves/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CasualLeave casualLeave = db.CasualLeaves.Find(id);
            if (casualLeave == null)
            {
                return HttpNotFound();
            }
            return View(casualLeave);
        }

        // GET: CasualLeaves/Create
        public ActionResult Create()
        {
            ViewBag.EmployeeCode = new SelectList(db.EmployeeGIs, "EmployeeCode", "Rank");
            ViewBag.EmployeeCode = new SelectList(db.EmployeeGIs, "EmployeeCode", "Rank");
            return View();
        }

        // POST: CasualLeaves/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,EmployeeCode,Name,Department,Post,FromDate,ToDate,Reason,ResponsiblePerson,HodComment,AnyLeaveDays,OfficeInChargeName,ApprovedDays,CreatedDate,IsDeleted")] CasualLeave casualLeave)
        {
            if (ModelState.IsValid)
            {
                db.CasualLeaves.Add(casualLeave);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EmployeeCode = new SelectList(db.EmployeeGIs, "EmployeeCode", "Rank", casualLeave.EmployeeCode);
            ViewBag.EmployeeCode = new SelectList(db.EmployeeGIs, "EmployeeCode", "Rank", casualLeave.EmployeeCode);
            return View(casualLeave);
        }

        // GET: CasualLeaves/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CasualLeave casualLeave = db.CasualLeaves.Find(id);
            if (casualLeave == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeCode = new SelectList(db.EmployeeGIs, "EmployeeCode", "Rank", casualLeave.EmployeeCode);
            ViewBag.EmployeeCode = new SelectList(db.EmployeeGIs, "EmployeeCode", "Rank", casualLeave.EmployeeCode);
            return View(casualLeave);
        }

        // POST: CasualLeaves/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,EmployeeCode,Name,Department,Post,FromDate,ToDate,Reason,ResponsiblePerson,HodComment,AnyLeaveDays,OfficeInChargeName,ApprovedDays,CreatedDate,IsDeleted")] CasualLeave casualLeave)
        {
            if (ModelState.IsValid)
            {
                db.Entry(casualLeave).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeCode = new SelectList(db.EmployeeGIs, "EmployeeCode", "Rank", casualLeave.EmployeeCode);
            ViewBag.EmployeeCode = new SelectList(db.EmployeeGIs, "EmployeeCode", "Rank", casualLeave.EmployeeCode);
            return View(casualLeave);
        }

        // GET: CasualLeaves/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CasualLeave casualLeave = db.CasualLeaves.Find(id);
            if (casualLeave == null)
            {
                return HttpNotFound();
            }
            return View(casualLeave);
        }

        // POST: CasualLeaves/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CasualLeave casualLeave = db.CasualLeaves.Find(id);
            db.CasualLeaves.Remove(casualLeave);
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
