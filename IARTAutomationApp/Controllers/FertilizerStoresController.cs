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
    public class FertilizerStoresController : Controller
    {
        private IARTDBNEWEntities db = new IARTDBNEWEntities();

        // GET: FertilizerStores
        public ActionResult Index()
        {
            var user = (UserMaster)Session["User"];
            return View(db.FertilizerStores.Where(e => e.CustomerId == user.CustomerId).ToList());
        }

        // GET: FertilizerStores/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FertilizerStore fertilizerStore = db.FertilizerStores.Find(id);
            if (fertilizerStore == null)
            {
                return HttpNotFound();
            }
            return View(fertilizerStore);
        }

        // GET: FertilizerStores/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FertilizerStores/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Item,Class,Price,CreatedDate,IsDeleted,CustomerId")] FertilizerStore fertilizerStore)
        {
            if (ModelState.IsValid)
            {
                db.FertilizerStores.Add(fertilizerStore);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(fertilizerStore);
        }

        // GET: FertilizerStores/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FertilizerStore fertilizerStore = db.FertilizerStores.Find(id);
            if (fertilizerStore == null)
            {
                return HttpNotFound();
            }
            return View(fertilizerStore);
        }

        // POST: FertilizerStores/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Item,Class,Price,CreatedDate,IsDeleted,CustomerId")] FertilizerStore fertilizerStore)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fertilizerStore).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(fertilizerStore);
        }

        // GET: FertilizerStores/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FertilizerStore fertilizerStore = db.FertilizerStores.Find(id);
            if (fertilizerStore == null)
            {
                return HttpNotFound();
            }
            return View(fertilizerStore);
        }

        // POST: FertilizerStores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FertilizerStore fertilizerStore = db.FertilizerStores.Find(id);
            db.FertilizerStores.Remove(fertilizerStore);
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
