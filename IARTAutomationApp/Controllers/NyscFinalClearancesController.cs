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

    public class NyscFinalClearancesController : Controller
    {
        private IARTDBNEWEntities db = new IARTDBNEWEntities();

        // GET: NyscFinalClearances
        public ActionResult Index()
        {
            var user = (IARTAutomationApp.Models.UserMaster)Session["User"];
            var nyscFinalClearances = db.NyscFinalClearances.Where(e => e.CustomerId == user.CustomerId).Include(n => n.EmployeeGI).Include(n => n.EmployeeGI1);
            return View(nyscFinalClearances.ToList());
        }

        // GET: NyscFinalClearances/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NyscFinalClearance nyscFinalClearance = db.NyscFinalClearances.Find(id);
            if (nyscFinalClearance == null)
            {
                return HttpNotFound();
            }
            return View(nyscFinalClearance);
        }

        // GET: NyscFinalClearances/Create
        public ActionResult Create()
        {
            ViewBag.EmployeeCode = new SelectList(db.EmployeeGIs, "EmployeeCode", "Rank");
            ViewBag.EmployeeCode = new SelectList(db.EmployeeGIs, "EmployeeCode", "Rank");
            return View();
        }

        // POST: NyscFinalClearances/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CustomerId,Id,EmployeeCode,OurRef,YourRef,Date,Name,NYSC_Code,EffectDate,BankAccountNo,CreatedDate,IsDeleted")] NyscFinalClearance nyscFinalClearance)
        {
            if (ModelState.IsValid)
            {
                db.NyscFinalClearances.Add(nyscFinalClearance);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EmployeeCode = new SelectList(db.EmployeeGIs, "EmployeeCode", "Rank", nyscFinalClearance.EmployeeCode);
            ViewBag.EmployeeCode = new SelectList(db.EmployeeGIs, "EmployeeCode", "Rank", nyscFinalClearance.EmployeeCode);
            return View(nyscFinalClearance);
        }

        // GET: NyscFinalClearances/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NyscFinalClearance nyscFinalClearance = db.NyscFinalClearances.Find(id);
            if (nyscFinalClearance == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeCode = new SelectList(db.EmployeeGIs, "EmployeeCode", "Rank", nyscFinalClearance.EmployeeCode);
            ViewBag.EmployeeCode = new SelectList(db.EmployeeGIs, "EmployeeCode", "Rank", nyscFinalClearance.EmployeeCode);
            return View(nyscFinalClearance);
        }

        // POST: NyscFinalClearances/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustomerId,Id,EmployeeCode,OurRef,YourRef,Date,Name,NYSC_Code,EffectDate,BankAccountNo,CreatedDate,IsDeleted")] NyscFinalClearance nyscFinalClearance)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nyscFinalClearance).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeCode = new SelectList(db.EmployeeGIs, "EmployeeCode", "Rank", nyscFinalClearance.EmployeeCode);
            ViewBag.EmployeeCode = new SelectList(db.EmployeeGIs, "EmployeeCode", "Rank", nyscFinalClearance.EmployeeCode);
            return View(nyscFinalClearance);
        }

        // GET: NyscFinalClearances/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NyscFinalClearance nyscFinalClearance = db.NyscFinalClearances.Find(id);
            if (nyscFinalClearance == null)
            {
                return HttpNotFound();
            }
            return View(nyscFinalClearance);
        }

        // POST: NyscFinalClearances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NyscFinalClearance nyscFinalClearance = db.NyscFinalClearances.Find(id);
            db.NyscFinalClearances.Remove(nyscFinalClearance);
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
