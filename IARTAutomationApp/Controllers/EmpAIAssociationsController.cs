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
    public class EmpAIAssociationsController : Controller
    {
        private IARTDBNEWEntities db = new IARTDBNEWEntities();

        // GET: EmpAIAssociations
        public ActionResult Index()
        {
            return View(db.EmpAIAssociations.ToList());
        }

        // GET: EmpAIAssociations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmpAIAssociation empAIAssociation = db.EmpAIAssociations.Find(id);
            if (empAIAssociation == null)
            {
                return HttpNotFound();
            }
            return View(empAIAssociation);
        }

        // GET: EmpAIAssociations/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EmpAIAssociations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AssociationsId,EmployeeCode,Title,IDnumber,AttendedDate,CreatedDate,IsDeleted")] EmpAIAssociation empAIAssociation)
        {
            if (ModelState.IsValid)
            {
                db.EmpAIAssociations.Add(empAIAssociation);
                db.SaveChanges();
 
            }
            return RedirectToAction("../EmployeeAIs/Create");

            //return View(empAIAssociation);
        }

        // GET: EmpAIAssociations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmpAIAssociation empAIAssociation = db.EmpAIAssociations.Find(id);
            if (empAIAssociation == null)
            {
                return HttpNotFound();
            }
            return View(empAIAssociation);
        }

        // POST: EmpAIAssociations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AssociationsId,EmployeeCode,Title,IDnumber,AttendedDate,CreatedDate,IsDeleted")] EmpAIAssociation empAIAssociation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(empAIAssociation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(empAIAssociation);
        }

        // GET: EmpAIAssociations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmpAIAssociation empAIAssociation = db.EmpAIAssociations.Find(id);
            if (empAIAssociation == null)
            {
                return HttpNotFound();
            }
            return View(empAIAssociation);
        }

        // POST: EmpAIAssociations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EmpAIAssociation empAIAssociation = db.EmpAIAssociations.Find(id);
            db.EmpAIAssociations.Remove(empAIAssociation);
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
