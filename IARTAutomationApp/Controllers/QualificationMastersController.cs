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
    
    public class QualificationMastersController : Controller
    {
        private IARTDBNEWEntities db = new IARTDBNEWEntities();

        // GET: QualificationMasters
        public ActionResult Index()
        {
            return View(db.QualificationMasters.ToList());
        }

        // GET: QualificationMasters/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QualificationMaster qualificationMaster = db.QualificationMasters.Find(id);
            if (qualificationMaster == null)
            {
                return HttpNotFound();
            }
            return View(qualificationMaster);
        }

        // GET: QualificationMasters/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: QualificationMasters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "QualificationId,QualificationName,Duration,CreatedDate,IsDeleted")] QualificationMaster qualificationMaster)
        {
            if (ModelState.IsValid)
            {
                db.QualificationMasters.Add(qualificationMaster);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(qualificationMaster);
        }

        // GET: QualificationMasters/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QualificationMaster qualificationMaster = db.QualificationMasters.Find(id);
            if (qualificationMaster == null)
            {
                return HttpNotFound();
            }
            return View(qualificationMaster);
        }

        // POST: QualificationMasters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "QualificationId,QualificationName,Duration,CreatedDate,IsDeleted")] QualificationMaster qualificationMaster)
        {
            if (ModelState.IsValid)
            {
                db.Entry(qualificationMaster).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(qualificationMaster);
        }

        // GET: QualificationMasters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QualificationMaster qualificationMaster = db.QualificationMasters.Find(id);
            if (qualificationMaster == null)
            {
                return HttpNotFound();
            }
            return View(qualificationMaster);
        }

        // POST: QualificationMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            QualificationMaster qualificationMaster = db.QualificationMasters.Find(id);
            db.QualificationMasters.Remove(qualificationMaster);
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
