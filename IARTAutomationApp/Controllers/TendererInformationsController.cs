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

    public class TendererInformationsController : Controller
    {
        private IARTDBNEWEntities db = new IARTDBNEWEntities();

        // GET: TendererInformations
        public ActionResult Index()
        {
            var user = (IARTAutomationApp.Models.UserMaster)Session["User"];

            ViewBag.TendererInformations = db.TendererInformations.Where(e => e.CustomerId == user.CustomerId).Count();
            return View(db.TendererInformations.ToList());
        }

        // GET: TendererInformations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TendererInformation tendererInformation = db.TendererInformations.Find(id);
            if (tendererInformation == null)
            {
                return HttpNotFound();
            }
            return View(tendererInformation);
        }

        // GET: TendererInformations/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TendererInformations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CustomerId,Id,CompanyName,LotNo,ProjectTitle,RepresentativeName,PhoneNo,SubmissionDate,YearofProject,CreatedDate,IsDeleted")] TendererInformation tendererInformation)
        {
            if (ModelState.IsValid)
            {
                tendererInformation.CreatedDate = DateTime.Now;

                db.TendererInformations.Add(tendererInformation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tendererInformation);
        }

        // GET: TendererInformations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TendererInformation tendererInformation = db.TendererInformations.Find(id);
            if (tendererInformation == null)
            {
                return HttpNotFound();
            }
            return View(tendererInformation);
        }

        // POST: TendererInformations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustomerId,Id,CompanyName,LotNo,ProjectTitle,RepresentativeName,PhoneNo,SubmissionDate,YearofProject,CreatedDate,IsDeleted")] TendererInformation tendererInformation)
        {
            if (ModelState.IsValid)
            {
                tendererInformation.CreatedDate = DateTime.Now;
                db.Entry(tendererInformation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tendererInformation);
        }

        // GET: TendererInformations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TendererInformation tendererInformation = db.TendererInformations.Find(id);
            if (tendererInformation == null)
            {
                return HttpNotFound();
            }
            return View(tendererInformation);
        }

        // POST: TendererInformations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TendererInformation tendererInformation = db.TendererInformations.Find(id);
            db.TendererInformations.Remove(tendererInformation);
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
