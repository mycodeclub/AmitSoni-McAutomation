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

    public class TenderOpeningsController : Controller
    {
        private IARTDBNEWEntities db = new IARTDBNEWEntities();

        // GET: TenderOpenings
        public ActionResult Index()
        {
            var user = (IARTAutomationApp.Models.UserMaster)Session["User"];
            var data = db.TenderOpenings.Where(e => e.CustomerId == user.CustomerId).ToList();
            ViewBag.TenderOpenings = data.Count();
            return View(data);
        }

        // GET: TenderOpenings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TenderOpening tenderOpening = db.TenderOpenings.Find(id);
            if (tenderOpening == null)
            {
                return HttpNotFound();
            }
            return View(tenderOpening);
        }

        // GET: TenderOpenings/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TenderOpenings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CustomerId,Id,RepresentativeName,CompanyName,AmountQuoted,CompletionPeriodFrom,CompletionPeriodTo,Remarks,LotNo,ProjectTitle,YearofProject,CreatedDate,IsDeleted")] TenderOpening tenderOpening)
        {
            if (ModelState.IsValid)
            {
                tenderOpening.CreatedDate = DateTime.Now;
                db.TenderOpenings.Add(tenderOpening);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tenderOpening);
        }

        // GET: TenderOpenings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TenderOpening tenderOpening = db.TenderOpenings.Find(id);
            if (tenderOpening == null)
            {
                return HttpNotFound();
            }
            return View(tenderOpening);
        }

        // POST: TenderOpenings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustomerId,Id,RepresentativeName,CompanyName,AmountQuoted,CompletionPeriodFrom,CompletionPeriodTo,Remarks,LotNo,ProjectTitle,YearofProject,CreatedDate,IsDeleted")] TenderOpening tenderOpening)
        {
            if (ModelState.IsValid)
            {
                tenderOpening.CreatedDate = DateTime.Now;
                db.Entry(tenderOpening).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tenderOpening);
        }

        // GET: TenderOpenings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TenderOpening tenderOpening = db.TenderOpenings.Find(id);
            if (tenderOpening == null)
            {
                return HttpNotFound();
            }
            return View(tenderOpening);
        }

        // POST: TenderOpenings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TenderOpening tenderOpening = db.TenderOpenings.Find(id);
            db.TenderOpenings.Remove(tenderOpening);
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
