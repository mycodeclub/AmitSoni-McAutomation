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
    public class DeductionsMastersController : Controller
    {
        private IARTDBNEWEntities db = new IARTDBNEWEntities();

        // GET: DeductionsMasters
        public ActionResult Index()
        {
            var user = (IARTAutomationApp.Models.UserMaster)Session["User"];
            return View(db.DeductionsMasters.Where(e => e.CustomerId == user.CustomerId).ToList());
        }

        // GET: DeductionsMasters/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DeductionsMaster deductionsMaster = db.DeductionsMasters.Find(id);
            if (deductionsMaster == null)
            {
                return HttpNotFound();
            }
            return View(deductionsMaster);
        }

        // GET: DeductionsMasters/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DeductionsMasters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CustomerId,DeductionId,DeductionType,ValueMethod,DeductionHead,DeductionAmount,IsActive,IsCreated,CreatedDate")] DeductionsMaster deductionsMaster)
        {
            if (ModelState.IsValid)
            {
                db.DeductionsMasters.Add(deductionsMaster);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(deductionsMaster);
        }

        // GET: DeductionsMasters/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DeductionsMaster deductionsMaster = db.DeductionsMasters.Find(id);
            if (deductionsMaster == null)
            {
                return HttpNotFound();
            }
            return View(deductionsMaster);
        }

        // POST: DeductionsMasters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustomerId,DeductionId,DeductionType,ValueMethod,DeductionHead,DeductionAmount,IsActive,IsCreated,CreatedDate")] DeductionsMaster deductionsMaster)
        {
            if (ModelState.IsValid)
            {
                db.Entry(deductionsMaster).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(deductionsMaster);
        }

        // GET: DeductionsMasters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DeductionsMaster deductionsMaster = db.DeductionsMasters.Find(id);
            if (deductionsMaster == null)
            {
                return HttpNotFound();
            }
            return View(deductionsMaster);
        }

        // POST: DeductionsMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DeductionsMaster deductionsMaster = db.DeductionsMasters.Find(id);
            db.DeductionsMasters.Remove(deductionsMaster);
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
