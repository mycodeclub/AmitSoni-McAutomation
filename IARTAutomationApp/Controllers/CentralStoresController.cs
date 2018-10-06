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
    public class CentralStoresController : Controller
    {
        private IARTDBNEWEntities db = new IARTDBNEWEntities();

        // GET: CentralStores
        public ActionResult Index()
        {
            var user = (UserMaster)Session["User"];
            return View(db.CentralStores.Where(e => e.CustomerId == user.CustomerId).ToList());
        }

        // GET: CentralStores/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CentralStore centralStore = db.CentralStores.Find(id);
            if (centralStore == null)
            {
                return HttpNotFound();
            }
            return View(centralStore);
        }

        // GET: CentralStores/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CentralStores/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Item,Class,Price,CreatedDate,IsDeleted,CustomerId")] CentralStore centralStore)
        {
            if (ModelState.IsValid)
            {
                db.CentralStores.Add(centralStore);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(centralStore);
        }

        // GET: CentralStores/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CentralStore centralStore = db.CentralStores.Find(id);
            if (centralStore == null)
            {
                return HttpNotFound();
            }
            return View(centralStore);
        }

        // POST: CentralStores/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Item,Class,Price,CreatedDate,IsDeleted,CustomerId")] CentralStore centralStore)
        {
            if (ModelState.IsValid)
            {
                db.Entry(centralStore).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(centralStore);
        }

        // GET: CentralStores/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CentralStore centralStore = db.CentralStores.Find(id);
            if (centralStore == null)
            {
                return HttpNotFound();
            }
            return View(centralStore);
        }

        // POST: CentralStores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CentralStore centralStore = db.CentralStores.Find(id);
            db.CentralStores.Remove(centralStore);
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
