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
    
    public class ProgrammeMastersController : Controller
    {
        private IARTDBNEWEntities db = new IARTDBNEWEntities();

        // GET: ProgrammeMasters
        public ActionResult Index()
        {
            ViewBag.ProgrammeMasters = db.ProgrammeMasters.Count();
            return View(db.ProgrammeMasters.ToList());
        }

        // GET: ProgrammeMasters/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProgrammeMaster programmeMaster = db.ProgrammeMasters.Find(id);
            if (programmeMaster == null)
            {
                return HttpNotFound();
            }
            return View(programmeMaster);
        }

        // GET: ProgrammeMasters/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProgrammeMasters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProgrammeId,ProgrammeName,CreatedDate,IsDeleted")] ProgrammeMaster programmeMaster)
        {
            if (ModelState.IsValid)
            {
                db.ProgrammeMasters.Add(programmeMaster);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(programmeMaster);
        }

        // GET: ProgrammeMasters/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProgrammeMaster programmeMaster = db.ProgrammeMasters.Find(id);
            if (programmeMaster == null)
            {
                return HttpNotFound();
            }
            return View(programmeMaster);
        }

        // POST: ProgrammeMasters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProgrammeId,ProgrammeName,CreatedDate,IsDeleted")] ProgrammeMaster programmeMaster)
        {
            if (ModelState.IsValid)
            {
                db.Entry(programmeMaster).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(programmeMaster);
        }

        // GET: ProgrammeMasters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProgrammeMaster programmeMaster = db.ProgrammeMasters.Find(id);
            if (programmeMaster == null)
            {
                return HttpNotFound();
            }
            return View(programmeMaster);
        }

        // POST: ProgrammeMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProgrammeMaster programmeMaster = db.ProgrammeMasters.Find(id);
            db.ProgrammeMasters.Remove(programmeMaster);
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
