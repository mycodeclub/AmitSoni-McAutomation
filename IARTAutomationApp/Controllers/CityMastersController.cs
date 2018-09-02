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
    public class CityMastersController : Controller
    {
        private IARTDBNEWEntities db = new IARTDBNEWEntities();

        // GET: CityMasters
        public ActionResult Index()
        {
            ViewBag.CityMasters = db.CityMasters.Count();
            return View(db.CityMasters.ToList());
        }

        // GET: CityMasters/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CityMaster cityMaster = db.CityMasters.Find(id);
            if (cityMaster == null)
            {
                return HttpNotFound();
            }
            return View(cityMaster);
        }

        // GET: CityMasters/Create
        public ActionResult Create()
        {
            List<StateMaster> StateList = new List<StateMaster>();
            StateList = (from State in db.StateMasters select State).ToList();
            //ViewBag.CountryList = CountryList;
            ViewBag.StateId = new SelectList(StateList, "Id", "State");
            return View();
        }

        // POST: CityMasters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,City,StateId,IsDeleted")] CityMaster cityMaster)
        {
            if (ModelState.IsValid)
            {
                db.CityMasters.Add(cityMaster);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cityMaster);
        }

        // GET: CityMasters/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CityMaster cityMaster = db.CityMasters.Find(id);
            if (cityMaster == null)
            {
                return HttpNotFound();
            }
            return View(cityMaster);
        }

        // POST: CityMasters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,City,StateId,IsDeleted")] CityMaster cityMaster)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cityMaster).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cityMaster);
        }

        // GET: CityMasters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CityMaster cityMaster = db.CityMasters.Find(id);
            if (cityMaster == null)
            {
                return HttpNotFound();
            }
            return View(cityMaster);
        }

        // POST: CityMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CityMaster cityMaster = db.CityMasters.Find(id);
            db.CityMasters.Remove(cityMaster);
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
