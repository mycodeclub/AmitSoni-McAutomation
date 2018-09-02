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
    public class EmpAIConferencesController : Controller
    {
        private IARTDBNEWEntities db = new IARTDBNEWEntities();

        // GET: EmpAIConferences
        public ActionResult Index()
        {
            return View(db.EmpAIConferences.ToList());
        }

        // GET: EmpAIConferences/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmpAIConference empAIConference = db.EmpAIConferences.Find(id);
            if (empAIConference == null)
            {
                return HttpNotFound();
            }
            return View(empAIConference);
        }

        // GET: EmpAIConferences/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EmpAIConferences/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ConferenceId,EmployeeCode,Name,Title,AttendedDate,CreatedDate,IsDeleted")] EmpAIConference empAIConference)
        {
            if (ModelState.IsValid)
            {
                db.EmpAIConferences.Add(empAIConference);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(empAIConference);
        }

        // GET: EmpAIConferences/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmpAIConference empAIConference = db.EmpAIConferences.Find(id);
            if (empAIConference == null)
            {
                return HttpNotFound();
            }
            return View(empAIConference);
        }

        // POST: EmpAIConferences/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ConferenceId,EmployeeCode,Name,Title,AttendedDate,CreatedDate,IsDeleted")] EmpAIConference empAIConference)
        {
            if (ModelState.IsValid)
            {
                db.Entry(empAIConference).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(empAIConference);
        }

        // GET: EmpAIConferences/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmpAIConference empAIConference = db.EmpAIConferences.Find(id);
            if (empAIConference == null)
            {
                return HttpNotFound();
            }
            return View(empAIConference);
        }

        // POST: EmpAIConferences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EmpAIConference empAIConference = db.EmpAIConferences.Find(id);
            db.EmpAIConferences.Remove(empAIConference);
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
