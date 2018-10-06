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

    public class StationMastersController : Controller
    {
        private IARTDBNEWEntities db = new IARTDBNEWEntities();

        // GET: StationMasters
        public ActionResult Index()
        {
            var user = (IARTAutomationApp.Models.UserMaster)Session["User"];
            var data = db.StationMasters.Where(e => e.CustomerId == user.CustomerId).ToList();
            ViewBag.StationMasters = db.StationMasters.Count();
            return View(data);
        }

        // GET: StationMasters/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StationMaster stationMaster = db.StationMasters.Find(id);
            if (stationMaster == null)
            {
                return HttpNotFound();
            }
            return View(stationMaster);
        }

        // GET: StationMasters/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StationMasters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CustomerId,StationId,StationName,CreatedDate,IsDeleted")] StationMaster stationMaster)
        {
            if (ModelState.IsValid)
            {
                db.StationMasters.Add(stationMaster);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(stationMaster);
        }

        // GET: StationMasters/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StationMaster stationMaster = db.StationMasters.Find(id);
            if (stationMaster == null)
            {
                return HttpNotFound();
            }
            return View(stationMaster);
        }

        // POST: StationMasters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustomerId,StationId,StationName,CreatedDate,IsDeleted")] StationMaster stationMaster)
        {
            if (ModelState.IsValid)
            {
                db.Entry(stationMaster).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(stationMaster);
        }

        // GET: StationMasters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StationMaster stationMaster = db.StationMasters.Find(id);
            if (stationMaster == null)
            {
                return HttpNotFound();
            }
            return View(stationMaster);
        }

        // POST: StationMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StationMaster stationMaster = db.StationMasters.Find(id);
            db.StationMasters.Remove(stationMaster);
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
