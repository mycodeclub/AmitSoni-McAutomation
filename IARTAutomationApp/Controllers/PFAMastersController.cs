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

    public class PFAMastersController : Controller
    {
        private IARTDBNEWEntities db = new IARTDBNEWEntities();

        // GET: PFAMasters
        public ActionResult Index()
        {
            var user = (IARTAutomationApp.Models.UserMaster)Session["User"];

            ViewBag.PFAMasters = db.PFAMasters.Where(e => e.CustomerId == user.CustomerId).Count();
            return View(db.PFAMasters.Where(e => e.CustomerId == user.CustomerId).ToList());
        }

        // GET: PFAMasters/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PFAMaster pFAMaster = db.PFAMasters.Find(id);
            if (pFAMaster == null)
            {
                return HttpNotFound();
            }
            return View(pFAMaster);
        }

        // GET: PFAMasters/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PFAMasters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CustomerId,PFAId,PFAName,CreatedDate,IsDeleted")] PFAMaster pFAMaster)
        {
            if (ModelState.IsValid)
            {
                db.PFAMasters.Add(pFAMaster);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pFAMaster);
        }

        // GET: PFAMasters/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PFAMaster pFAMaster = db.PFAMasters.Find(id);
            if (pFAMaster == null)
            {
                return HttpNotFound();
            }
            return View(pFAMaster);
        }

        // POST: PFAMasters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustomerId,PFAId,PFAName,CreatedDate,IsDeleted")] PFAMaster pFAMaster)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pFAMaster).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pFAMaster);
        }

        // GET: PFAMasters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PFAMaster pFAMaster = db.PFAMasters.Find(id);
            if (pFAMaster == null)
            {
                return HttpNotFound();
            }
            return View(pFAMaster);
        }

        // POST: PFAMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PFAMaster pFAMaster = db.PFAMasters.Find(id);
            db.PFAMasters.Remove(pFAMaster);
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
