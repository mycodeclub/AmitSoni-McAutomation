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

    public class UnitServicesMastersController : Controller
    {
        private IARTDBNEWEntities db = new IARTDBNEWEntities();

        // GET: UnitServicesMasters
        public ActionResult Index()
        {
            var user = (IARTAutomationApp.Models.UserMaster)Session["User"];
            var data = db.UnitServicesMasters.Where(e => e.CustomerId == user.CustomerId).ToList();
            ViewBag.UnitServicesMasters = data.Count();
            return View(data);
        }

        // GET: UnitServicesMasters/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UnitServicesMaster unitServicesMaster = db.UnitServicesMasters.Find(id);
            if (unitServicesMaster == null)
            {
                return HttpNotFound();
            }
            return View(unitServicesMaster);
        }

        // GET: UnitServicesMasters/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UnitServicesMasters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CustomerId,UnitServicesId,UnitServicesName,CreatedDate,IsDeleted")] UnitServicesMaster unitServicesMaster)
        {
            if (ModelState.IsValid)
            {
                unitServicesMaster.CreatedDate = DateTime.Now;
                db.UnitServicesMasters.Add(unitServicesMaster);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(unitServicesMaster);
        }

        // GET: UnitServicesMasters/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UnitServicesMaster unitServicesMaster = db.UnitServicesMasters.Find(id);
            if (unitServicesMaster == null)
            {
                return HttpNotFound();
            }
            return View(unitServicesMaster);
        }

        // POST: UnitServicesMasters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustomerId,UnitServicesId,UnitServicesName,CreatedDate,IsDeleted")] UnitServicesMaster unitServicesMaster)
        {
            if (ModelState.IsValid)
            {
                unitServicesMaster.CreatedDate = DateTime.Now;
                db.Entry(unitServicesMaster).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(unitServicesMaster);
        }

        // GET: UnitServicesMasters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UnitServicesMaster unitServicesMaster = db.UnitServicesMasters.Find(id);
            if (unitServicesMaster == null)
            {
                return HttpNotFound();
            }
            return View(unitServicesMaster);
        }

        // POST: UnitServicesMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UnitServicesMaster unitServicesMaster = db.UnitServicesMasters.Find(id);
            db.UnitServicesMasters.Remove(unitServicesMaster);
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
