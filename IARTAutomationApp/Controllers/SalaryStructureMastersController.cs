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
    
    public class SalaryStructureMastersController : Controller
    {
        private IARTDBNEWEntities db = new IARTDBNEWEntities();

        // GET: SalaryStructureMasters
        public ActionResult Index()
        {
            return View(db.SalaryStructureMasters.ToList());
        }

        // GET: SalaryStructureMasters/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalaryStructureMaster salaryStructureMaster = db.SalaryStructureMasters.Find(id);
            if (salaryStructureMaster == null)
            {
                return HttpNotFound();
            }
            return View(salaryStructureMaster);
        }

        // GET: SalaryStructureMasters/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SalaryStructureMasters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SalaryScaleId,SalaryScale,GradeLevel,Step,SalaryAmount,IsActive,IsDeleted,ScaleYear,CreatedDate")] SalaryStructureMaster salaryStructureMaster)
        {
            if (ModelState.IsValid)
            {
                db.SalaryStructureMasters.Add(salaryStructureMaster);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(salaryStructureMaster);
        }

        // GET: SalaryStructureMasters/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalaryStructureMaster salaryStructureMaster = db.SalaryStructureMasters.Find(id);
            if (salaryStructureMaster == null)
            {
                return HttpNotFound();
            }
            return View(salaryStructureMaster);
        }

        // POST: SalaryStructureMasters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SalaryScaleId,SalaryScale,GradeLevel,Step,SalaryAmount,IsActive,IsDeleted,ScaleYear,CreatedDate")] SalaryStructureMaster salaryStructureMaster)
        {
            if (ModelState.IsValid)
            {
                db.Entry(salaryStructureMaster).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(salaryStructureMaster);
        }

        // GET: SalaryStructureMasters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalaryStructureMaster salaryStructureMaster = db.SalaryStructureMasters.Find(id);
            if (salaryStructureMaster == null)
            {
                return HttpNotFound();
            }
            return View(salaryStructureMaster);
        }

        // POST: SalaryStructureMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SalaryStructureMaster salaryStructureMaster = db.SalaryStructureMasters.Find(id);
            db.SalaryStructureMasters.Remove(salaryStructureMaster);
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
