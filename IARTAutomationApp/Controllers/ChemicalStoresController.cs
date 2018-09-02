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
    public class ChemicalStoresController : Controller
    {
        private IARTDBNEWEntities db = new IARTDBNEWEntities();

        // GET: ChemicalStores
        public ActionResult Index()
        {
            return View(db.ChemicalStores.ToList());
        }

        // GET: ChemicalStores/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChemicalStore chemicalStore = db.ChemicalStores.Find(id);
            if (chemicalStore == null)
            {
                return HttpNotFound();
            }
            return View(chemicalStore);
        }

        // GET: ChemicalStores/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ChemicalStores/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Item,Class,Price,CreatedDate,IsDeleted")] ChemicalStore chemicalStore)
        {
            if (ModelState.IsValid)
            {
                db.ChemicalStores.Add(chemicalStore);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(chemicalStore);
        }

        // GET: ChemicalStores/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChemicalStore chemicalStore = db.ChemicalStores.Find(id);
            if (chemicalStore == null)
            {
                return HttpNotFound();
            }
            return View(chemicalStore);
        }

        // POST: ChemicalStores/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Item,Class,Price,CreatedDate,IsDeleted")] ChemicalStore chemicalStore)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chemicalStore).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(chemicalStore);
        }

        // GET: ChemicalStores/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChemicalStore chemicalStore = db.ChemicalStores.Find(id);
            if (chemicalStore == null)
            {
                return HttpNotFound();
            }
            return View(chemicalStore);
        }

        // POST: ChemicalStores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ChemicalStore chemicalStore = db.ChemicalStores.Find(id);
            db.ChemicalStores.Remove(chemicalStore);
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
