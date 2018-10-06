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

    public class LeaveLedgersController : Controller
    {
        private IARTDBNEWEntities db = new IARTDBNEWEntities();

        // GET: LeaveLedgers
        public ActionResult Index()
        {
            var user = (IARTAutomationApp.Models.UserMaster)Session["User"];
            return View(db.LeaveLedgers.Where(e => e.CustomerId == user.CustomerId).ToList().OrderByDescending(k => k.LeaveLogId));
        }

        // GET: LeaveLedgers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LeaveLedger leaveLedger = db.LeaveLedgers.Find(id);
            if (leaveLedger == null)
            {
                return HttpNotFound();
            }
            return View(leaveLedger);
        }

        // GET: LeaveLedgers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LeaveLedgers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CustomerId,LeaveLogId,EmployeeCode,LeaveType,ConsumedLeaves,BalanceLeaves,FiscalYear")] LeaveLedger leaveLedger)
        {
            if (ModelState.IsValid)
            {
                db.LeaveLedgers.Add(leaveLedger);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(leaveLedger);
        }

        // GET: LeaveLedgers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LeaveLedger leaveLedger = db.LeaveLedgers.Find(id);
            if (leaveLedger == null)
            {
                return HttpNotFound();
            }
            return View(leaveLedger);
        }

        // POST: LeaveLedgers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustomerId,LeaveLogId,EmployeeCode,LeaveType,ConsumedLeaves,BalanceLeaves,FiscalYear")] LeaveLedger leaveLedger)
        {
            if (ModelState.IsValid)
            {
                db.Entry(leaveLedger).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(leaveLedger);
        }

        // GET: LeaveLedgers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LeaveLedger leaveLedger = db.LeaveLedgers.Find(id);
            if (leaveLedger == null)
            {
                return HttpNotFound();
            }
            return View(leaveLedger);
        }

        // POST: LeaveLedgers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LeaveLedger leaveLedger = db.LeaveLedgers.Find(id);
            db.LeaveLedgers.Remove(leaveLedger);
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
