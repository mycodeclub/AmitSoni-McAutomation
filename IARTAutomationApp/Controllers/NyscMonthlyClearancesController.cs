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

    public class NyscMonthlyClearancesController : Controller
    {
        private IARTDBNEWEntities db = new IARTDBNEWEntities();

        // GET: NyscMonthlyClearances
        public ActionResult Index()
        {
            var user = (IARTAutomationApp.Models.UserMaster)Session["User"];
            var nyscMonthlyClearances = db.NyscMonthlyClearances.Where(e => e.CustomerId == user.CustomerId).Include(n => n.EmployeeGI).Include(n => n.EmployeeGI1);
            return View(nyscMonthlyClearances.ToList());
        }

        // GET: NyscMonthlyClearances/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NyscMonthlyClearance nyscMonthlyClearance = db.NyscMonthlyClearances.Find(id);
            if (nyscMonthlyClearance == null)
            {
                return HttpNotFound();
            }
            return View(nyscMonthlyClearance);
        }

        // GET: NyscMonthlyClearances/Create
        public ActionResult Create()
        {
            ViewBag.EmployeeCode = new SelectList(db.EmployeeGIs, "EmployeeCode", "Rank");
            ViewBag.EmployeeCode = new SelectList(db.EmployeeGIs, "EmployeeCode", "Rank");
            return View();
        }

        // POST: NyscMonthlyClearances/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CustomerId,Id,EmployeeCode,OurRef,YourRef,Date,Name,NYSC_Code,SatisfactoryMonth,AllowanceMonth,CreatedDate,IsDeleted")] NyscMonthlyClearance nyscMonthlyClearance)
        {
            if (ModelState.IsValid)
            {
                db.NyscMonthlyClearances.Add(nyscMonthlyClearance);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EmployeeCode = new SelectList(db.EmployeeGIs, "EmployeeCode", "Rank", nyscMonthlyClearance.EmployeeCode);
            ViewBag.EmployeeCode = new SelectList(db.EmployeeGIs, "EmployeeCode", "Rank", nyscMonthlyClearance.EmployeeCode);
            return View(nyscMonthlyClearance);
        }

        // GET: NyscMonthlyClearances/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NyscMonthlyClearance nyscMonthlyClearance = db.NyscMonthlyClearances.Find(id);
            if (nyscMonthlyClearance == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeCode = new SelectList(db.EmployeeGIs, "EmployeeCode", "Rank", nyscMonthlyClearance.EmployeeCode);
            ViewBag.EmployeeCode = new SelectList(db.EmployeeGIs, "EmployeeCode", "Rank", nyscMonthlyClearance.EmployeeCode);
            return View(nyscMonthlyClearance);
        }

        // POST: NyscMonthlyClearances/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustomerId,Id,EmployeeCode,OurRef,YourRef,Date,Name,NYSC_Code,SatisfactoryMonth,AllowanceMonth,CreatedDate,IsDeleted")] NyscMonthlyClearance nyscMonthlyClearance)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nyscMonthlyClearance).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeCode = new SelectList(db.EmployeeGIs, "EmployeeCode", "Rank", nyscMonthlyClearance.EmployeeCode);
            ViewBag.EmployeeCode = new SelectList(db.EmployeeGIs, "EmployeeCode", "Rank", nyscMonthlyClearance.EmployeeCode);
            return View(nyscMonthlyClearance);
        }

        // GET: NyscMonthlyClearances/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NyscMonthlyClearance nyscMonthlyClearance = db.NyscMonthlyClearances.Find(id);
            if (nyscMonthlyClearance == null)
            {
                return HttpNotFound();
            }
            return View(nyscMonthlyClearance);
        }

        // POST: NyscMonthlyClearances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NyscMonthlyClearance nyscMonthlyClearance = db.NyscMonthlyClearances.Find(id);
            db.NyscMonthlyClearances.Remove(nyscMonthlyClearance);
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
