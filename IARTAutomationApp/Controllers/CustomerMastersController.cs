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
    [Authorize(Roles = "SuperAdmin")]

    public class CustomerMastersController : Controller
    {

        private IARTDBNEWEntities db = new IARTDBNEWEntities();

        // GET: CustomerMasters
        public ActionResult Index()
        {
            return View(db.CustomerMasters.ToList());
        }

        // GET: CustomerMasters/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerMaster customerMaster = db.CustomerMasters.Find(id);
            if (customerMaster == null)
            {
                return HttpNotFound();
            }
            return View(customerMaster);
        }

        // GET: CustomerMasters/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CustomerMasters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CustomerId,Name,LoginName,Password,Phone,Email,Website")] CustomerMaster customerMaster)
        {
            if (ModelState.IsValid)
            {
                db.CustomerMasters.Add(customerMaster);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(customerMaster);
        }

        // GET: CustomerMasters/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerMaster customerMaster = db.CustomerMasters.Find(id);
            if (customerMaster == null)
            {
                return HttpNotFound();
            }
            return View(customerMaster);
        }

        // POST: CustomerMasters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustomerId,Name,LoginName,Password,Phone,Email,Website")] CustomerMaster customerMaster)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customerMaster).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customerMaster);
        }

        // GET: CustomerMasters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerMaster customerMaster = db.CustomerMasters.Find(id);
            if (customerMaster == null)
            {
                return HttpNotFound();
            }
            return View(customerMaster);
        }

        // POST: CustomerMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CustomerMaster customerMaster = db.CustomerMasters.Find(id);
            db.CustomerMasters.Remove(customerMaster);
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
