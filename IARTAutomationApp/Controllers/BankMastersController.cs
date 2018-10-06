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
    public class BankMastersController : Controller
    {
        private IARTDBNEWEntities db = new IARTDBNEWEntities();

        // GET: BankMasters
        public ActionResult Index()
        {
            var user = (UserMaster)Session["User"];

            ViewBag.BankMasters = db.BankMasters.Where(e => e.CustomerId == user.CustomerId).Count();
            return View(db.BankMasters.ToList());
        }

        // GET: BankMasters/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BankMaster bankMaster = db.BankMasters.Find(id);
            if (bankMaster == null)
            {
                return HttpNotFound();
            }
            return View(bankMaster);
        }

        // GET: BankMasters/Create
        public ActionResult Create()
        {
            List<BankTypeMaster> BankTypeList = new List<BankTypeMaster>();
            BankTypeList = (from State in db.BankTypeMasters select State).ToList();
            //ViewBag.CountryList = CountryList;
            ViewBag.BankTypeId = new SelectList(BankTypeList, "BankTypeId", "BankTypeName");
            return View();
        }

        // POST: BankMasters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CustomerId,BankId,BankName,BankTypeId,CreatedDate,IsDeleted")] BankMaster bankMaster)
        {
            if (ModelState.IsValid)
            {
                db.BankMasters.Add(bankMaster);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bankMaster);
        }

        // GET: BankMasters/Edit/5
        public ActionResult Edit(int? id)
        {
            List<BankTypeMaster> BankTypeList = new List<BankTypeMaster>();
            BankTypeList = (from State in db.BankTypeMasters select State).ToList();
            //ViewBag.CountryList = CountryList;
            ViewBag.BankTypeId = new SelectList(BankTypeList, "BankTypeId", "BankTypeName");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BankMaster bankMaster = db.BankMasters.Find(id);
            if (bankMaster == null)
            {
                return HttpNotFound();
            }
            return View(bankMaster);
        }

        // POST: BankMasters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustomerId,BankId,BankName,BankTypeId,CreatedDate,IsDeleted")] BankMaster bankMaster)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bankMaster).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bankMaster);
        }

        // GET: BankMasters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BankMaster bankMaster = db.BankMasters.Find(id);
            if (bankMaster == null)
            {
                return HttpNotFound();
            }
            return View(bankMaster);
        }

        // POST: BankMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BankMaster bankMaster = db.BankMasters.Find(id);
            db.BankMasters.Remove(bankMaster);
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
