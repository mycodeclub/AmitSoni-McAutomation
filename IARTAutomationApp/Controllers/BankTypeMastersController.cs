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
    public class BankTypeMastersController : Controller
    {
        private IARTDBNEWEntities db = new IARTDBNEWEntities();

        // GET: BankTypeMasters
        public ActionResult Index()
        {
            ViewBag.BankType = db.BankTypeMasters.Count();
            return View(db.BankTypeMasters.ToList());
        }

        // GET: BankTypeMasters/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BankTypeMaster bankTypeMaster = db.BankTypeMasters.Find(id);
            if (bankTypeMaster == null)
            {
                return HttpNotFound();
            }
            return View(bankTypeMaster);
        }

        // GET: BankTypeMasters/Create
        public ActionResult Create()
        {


            return View();
        }

        // POST: BankTypeMasters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BankTypeId,BankTypeName,CreatedDate,IsDeleted")] BankTypeMaster bankTypeMaster)
        {
            if (ModelState.IsValid)
            {
                db.BankTypeMasters.Add(bankTypeMaster);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bankTypeMaster);
        }

        // GET: BankTypeMasters/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BankTypeMaster bankTypeMaster = db.BankTypeMasters.Find(id);
            if (bankTypeMaster == null)
            {
                return HttpNotFound();
            }
            return View(bankTypeMaster);
        }

        // POST: BankTypeMasters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BankTypeId,BankTypeName,CreatedDate,IsDeleted")] BankTypeMaster bankTypeMaster)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bankTypeMaster).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bankTypeMaster);
        }

        // GET: BankTypeMasters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BankTypeMaster bankTypeMaster = db.BankTypeMasters.Find(id);
            if (bankTypeMaster == null)
            {
                return HttpNotFound();
            }
            return View(bankTypeMaster);
        }

        // POST: BankTypeMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BankTypeMaster bankTypeMaster = db.BankTypeMasters.Find(id);
            db.BankTypeMasters.Remove(bankTypeMaster);
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
