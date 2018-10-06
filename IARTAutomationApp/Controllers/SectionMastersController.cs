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

    public class SectionMastersController : Controller
    {
        private IARTDBNEWEntities db = new IARTDBNEWEntities();

        // GET: SectionMasters
        public ActionResult Index()
        {
            var user = (IARTAutomationApp.Models.UserMaster)Session["User"];
            var data = db.SectionMasters.Where(e => e.CustomerId == user.CustomerId).ToList();
            ViewBag.SectionMasters = data.Count();
            return View(data);
        }

        // GET: SectionMasters/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SectionMaster sectionMaster = db.SectionMasters.Find(id);
            if (sectionMaster == null)
            {
                return HttpNotFound();
            }
            return View(sectionMaster);
        }

        // GET: SectionMasters/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SectionMasters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CustomerId,SectionId,SectionName,CreatedDate,IsDeleted")] SectionMaster sectionMaster)
        {
            if (ModelState.IsValid)
            {
                db.SectionMasters.Add(sectionMaster);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sectionMaster);
        }

        // GET: SectionMasters/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SectionMaster sectionMaster = db.SectionMasters.Find(id);
            if (sectionMaster == null)
            {
                return HttpNotFound();
            }
            return View(sectionMaster);
        }

        // POST: SectionMasters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustomerId,SectionId,SectionName,CreatedDate,IsDeleted")] SectionMaster sectionMaster)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sectionMaster).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sectionMaster);
        }

        // GET: SectionMasters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SectionMaster sectionMaster = db.SectionMasters.Find(id);
            if (sectionMaster == null)
            {
                return HttpNotFound();
            }
            return View(sectionMaster);
        }

        // POST: SectionMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SectionMaster sectionMaster = db.SectionMasters.Find(id);
            db.SectionMasters.Remove(sectionMaster);
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
