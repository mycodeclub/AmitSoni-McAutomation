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
    public class CadreMastersController : Controller
    {
        private IARTDBNEWEntities db = new IARTDBNEWEntities();

        // GET: CadreMasters
        public ActionResult Index()
        {
            ViewBag.CadreMasters = db.CadreMasters.Count();
            return View(db.CadreMasters.ToList());
        }

        // GET: CadreMasters/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CadreMaster cadreMaster = db.CadreMasters.Find(id);
            if (cadreMaster == null)
            {
                return HttpNotFound();
            }
            return View(cadreMaster);
        }

        // GET: CadreMasters/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CadreMasters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CadreId,CadreName,CreatedDate,IsDeleted")] CadreMaster cadreMaster)
        {
            if (ModelState.IsValid)
            {
                db.CadreMasters.Add(cadreMaster);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cadreMaster);
        }

        // GET: CadreMasters/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CadreMaster cadreMaster = db.CadreMasters.Find(id);
            if (cadreMaster == null)
            {
                return HttpNotFound();
            }
            return View(cadreMaster);
        }

        // POST: CadreMasters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CadreId,CadreName,CreatedDate,IsDeleted")] CadreMaster cadreMaster)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cadreMaster).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cadreMaster);
        }

        // GET: CadreMasters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CadreMaster cadreMaster = db.CadreMasters.Find(id);
            if (cadreMaster == null)
            {
                return HttpNotFound();
            }
            return View(cadreMaster);
        }

        // POST: CadreMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CadreMaster cadreMaster = db.CadreMasters.Find(id);
            db.CadreMasters.Remove(cadreMaster);
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
