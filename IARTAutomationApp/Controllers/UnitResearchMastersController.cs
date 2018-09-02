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
    
    public class UnitResearchMastersController : Controller
    {
        private IARTDBNEWEntities db = new IARTDBNEWEntities();

        // GET: UnitResearchMasters
        public ActionResult Index()
        {
            ViewBag.UnitResearchMasters = db.UnitResearchMasters.Count();
            return View(db.UnitResearchMasters.ToList());
        }

        // GET: UnitResearchMasters/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UnitResearchMaster unitResearchMaster = db.UnitResearchMasters.Find(id);
            if (unitResearchMaster == null)
            {
                return HttpNotFound();
            }
            return View(unitResearchMaster);
        }

        // GET: UnitResearchMasters/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UnitResearchMasters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UnitResearchId,UnitResearchName,CreatedDate,IsDeleted")] UnitResearchMaster unitResearchMaster)
        {
            if (ModelState.IsValid)
            {
                db.UnitResearchMasters.Add(unitResearchMaster);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(unitResearchMaster);
        }

        // GET: UnitResearchMasters/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UnitResearchMaster unitResearchMaster = db.UnitResearchMasters.Find(id);
            if (unitResearchMaster == null)
            {
                return HttpNotFound();
            }
            return View(unitResearchMaster);
        }

        // POST: UnitResearchMasters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UnitResearchId,UnitResearchName,CreatedDate,IsDeleted")] UnitResearchMaster unitResearchMaster)
        {
            if (ModelState.IsValid)
            {
                db.Entry(unitResearchMaster).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(unitResearchMaster);
        }

        // GET: UnitResearchMasters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UnitResearchMaster unitResearchMaster = db.UnitResearchMasters.Find(id);
            if (unitResearchMaster == null)
            {
                return HttpNotFound();
            }
            return View(unitResearchMaster);
        }

        // POST: UnitResearchMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UnitResearchMaster unitResearchMaster = db.UnitResearchMasters.Find(id);
            db.UnitResearchMasters.Remove(unitResearchMaster);
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
