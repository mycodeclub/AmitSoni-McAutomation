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
    public class AllowanceMastersController : Controller
    {
        private IARTDBNEWEntities db = new IARTDBNEWEntities();

        // GET: AllowanceMasters
        public ActionResult Index()
        {
            var user = (UserMaster)Session["User"];
            return View(db.AllowanceMasters.Where(e => e.CustomerId == user.CustomerId).ToList());
        }

        // GET: AllowanceMasters/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AllowanceMaster allowanceMaster = db.AllowanceMasters.Find(id);
            if (allowanceMaster == null)
            {
                return HttpNotFound();
            }
            return View(allowanceMaster);
        }

        // GET: AllowanceMasters/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AllowanceMasters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AllowanceId,AllowanceType,ValueMethod,AllowanceHead,AllowanceAmount,IsActive,IsCreated,CreatedDate,CustomerId")] AllowanceMaster allowanceMaster)
        {
            if (ModelState.IsValid)
            {
                db.AllowanceMasters.Add(allowanceMaster);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(allowanceMaster);
        }

        // GET: AllowanceMasters/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AllowanceMaster allowanceMaster = db.AllowanceMasters.Find(id);
            if (allowanceMaster == null)
            {
                return HttpNotFound();
            }
            return View(allowanceMaster);
        }

        // POST: AllowanceMasters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AllowanceId,AllowanceType,ValueMethod,AllowanceHead,AllowanceAmount,IsActive,IsCreated,CreatedDate,CustomerId")] AllowanceMaster allowanceMaster)
        {
            if (ModelState.IsValid)
            {
                db.Entry(allowanceMaster).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(allowanceMaster);
        }

        // GET: AllowanceMasters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AllowanceMaster allowanceMaster = db.AllowanceMasters.Find(id);
            if (allowanceMaster == null)
            {
                return HttpNotFound();
            }
            return View(allowanceMaster);
        }

        // POST: AllowanceMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AllowanceMaster allowanceMaster = db.AllowanceMasters.Find(id);
            db.AllowanceMasters.Remove(allowanceMaster);
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
