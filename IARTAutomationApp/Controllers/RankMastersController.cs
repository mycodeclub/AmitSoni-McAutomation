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
    
    public class RankMastersController : Controller
    {
        private IARTDBNEWEntities db = new IARTDBNEWEntities();

        // GET: RankMasters
        public ActionResult Index()
        {
            ViewBag.TotalRanks = db.RankMasters.Count();
            return View(db.RankMasters.ToList());
        }

        // GET: RankMasters/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RankMaster rankMaster = db.RankMasters.Find(id);
            if (rankMaster == null)
            {
                return HttpNotFound();
            }
            return View(rankMaster);
        }

        // GET: RankMasters/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RankMasters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RankId,RankName,RankDescription,CreatedDate,IsDeleted")] RankMaster rankMaster)
        {
            if (ModelState.IsValid)
            {
                db.RankMasters.Add(rankMaster);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(rankMaster);
        }

        // GET: RankMasters/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RankMaster rankMaster = db.RankMasters.Find(id);
            if (rankMaster == null)
            {
                return HttpNotFound();
            }
            return View(rankMaster);
        }

        // POST: RankMasters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RankId,RankName,RankDescription,CreatedDate,IsDeleted")] RankMaster rankMaster)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rankMaster).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(rankMaster);
        }

        // GET: RankMasters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RankMaster rankMaster = db.RankMasters.Find(id);
            if (rankMaster == null)
            {
                return HttpNotFound();
            }
            return View(rankMaster);
        }

        // POST: RankMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RankMaster rankMaster = db.RankMasters.Find(id);
            db.RankMasters.Remove(rankMaster);
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
