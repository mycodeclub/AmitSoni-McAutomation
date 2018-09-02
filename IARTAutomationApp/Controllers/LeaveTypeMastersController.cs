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
    
    public class LeaveTypeMastersController : Controller
    {
        private IARTDBNEWEntities db = new IARTDBNEWEntities();

        // GET: LeaveTypeMasters
        public ActionResult Index()
        {
            return View(db.LeaveTypeMasters.ToList());
        }

        // GET: LeaveTypeMasters/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LeaveTypeMaster leaveTypeMaster = db.LeaveTypeMasters.Find(id);
            if (leaveTypeMaster == null)
            {
                return HttpNotFound();
            }
            return View(leaveTypeMaster);
        }

        // GET: LeaveTypeMasters/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LeaveTypeMasters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LeaveTypeId,LeaveTypeName,IsActive,IsDeleted,CreatedDate")] LeaveTypeMaster leaveTypeMaster)
        {
            if (ModelState.IsValid)
            {
                db.LeaveTypeMasters.Add(leaveTypeMaster);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(leaveTypeMaster);
        }

        // GET: LeaveTypeMasters/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LeaveTypeMaster leaveTypeMaster = db.LeaveTypeMasters.Find(id);
            if (leaveTypeMaster == null)
            {
                return HttpNotFound();
            }
            return View(leaveTypeMaster);
        }

        // POST: LeaveTypeMasters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LeaveTypeId,LeaveTypeName,IsActive,IsDeleted,CreatedDate")] LeaveTypeMaster leaveTypeMaster)
        {
            if (ModelState.IsValid)
            {
                db.Entry(leaveTypeMaster).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(leaveTypeMaster);
        }

        // GET: LeaveTypeMasters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LeaveTypeMaster leaveTypeMaster = db.LeaveTypeMasters.Find(id);
            if (leaveTypeMaster == null)
            {
                return HttpNotFound();
            }
            return View(leaveTypeMaster);
        }

        // POST: LeaveTypeMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LeaveTypeMaster leaveTypeMaster = db.LeaveTypeMasters.Find(id);
            db.LeaveTypeMasters.Remove(leaveTypeMaster);
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
